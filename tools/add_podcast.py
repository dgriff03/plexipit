import argparse
import feedparser
import io
import os
import re
from datetime import datetime


def getImageHelper_(obj):
    img = None
    if obj.get('image'):
        img = feed.get('image').get('url')
    return img


def getImage(feed):
    thumbnail = getImageHelper_(feed['feed'])
    if not thumbnail and feed.get('channel'):
        thumbnail = getImageHelper_(feed.get('channel'))
    return thumbnail


def GetAudioLink(links, meme_type):
    for link in links:
        if link['type'] == meme_type:
            return link['href']

def GetSecondDurationString(entry):
    duration = entry.get('itunes_duration', '0')
    splitTime = s.split(':')
    mins = 0
    secs = 0
    secs = int(splitTime[-1])
    if len(splitTime) > 1:
        mins = int(splits[0]) * 60
    if len(splitTime) > 2:
        mins *= 60
        mins += int(splits[0])
    second_duration = mins * 60 + secs
    return str(second_duration)


def GetEpisodeRelease(entry):
    release_date = datetime.now()
    if entry['published']:
        try:
            release_date = datetime.strptime(entry['published'][:-6], '%a, %d %b %Y %H:%M:%S')
        except ValueError:
            release_date = datetime.strptime(entry['published'][:-13], '%a, %d %b %Y')
    return release_date


def buildEpisodeList(feed):
    episodes = []
    now = datetime.now()
    for entry in feed['entries']:
        data = {}
        data['audio_link'] = GetAudioLink(entry['links'], 'audio/mpeg')
        data['release_date'] = GetEpisodeRelease(entry)
        data['created'] = datetime.now()
        data['name'] = entry['title'].encode('ascii', 'ignore')
        data['length'] = GetSecondDurationString(entry)
        data['last_updated'] = now
        data['created'] = now
        # TODO(dgriff): Add episode number, description, and size
        episodes.append(data.copy())
    return episodes


def addPodcast(rss):
    feed = feedparser.parse(rss)
    feedObj = feed['feed']
    now = datetime.now()

    # TODO(dgriff): Block on podcast already exisiting

    feed_links = [link for link in feedObj['links'] if u['type'] == 'text/html']
    website = feed_links[0]['href'] if len(feed_links) > 0 else ''

    data = {}
    data['created'] = now
    data['rss'] = rss_url
    data['website'] = website
    data['name'] = feedObj['title']
    data['last_fetched'] = now
    data['last_updated'] = now
    if feedObj.get('publisher_detail'):
        data['producer'] = feedObj['publisher_detail'].get('name')
    img = getImage(feed)
    if img:
        data['image_link'] = img

    # TODO(dgriff): set network and description

    # TODO(dgriff): Creative podcast from data

    episodes = buildEpisodeList(feed)

    # TODO(dgriff): Add podcast ID to all episodes
    # TODO(dgriff): Filter for episodes that exist
    # TODO(dgriff): Create episode for remaining

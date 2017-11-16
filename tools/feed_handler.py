import argparse
import feedparser
import io
import os
import re
from datetime import datetime
import db.model as db


def formatDatetime_(dt):
    return datetime.strptime(dt.strftime('%Y-%m-%d %H:%M:%S'), '%Y-%m-%d %H:%M:%S')


def getImageHelper_(obj):
    img = None
    if obj.get('image'):
        img = obj.get('image').get('url')
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
    splitTime = duration.split(':')
    mins = 0
    secs = 0
    secs = int(splitTime[-1])
    if len(splitTime) > 1:
        mins = int(splitTime[0]) * 60
    if len(splitTime) > 2:
        mins *= 60
        mins += int(splitTime[1])
    second_duration = mins * 60 + secs
    return str(second_duration)


def GetEpisodeRelease(entry, now):
    release_date = now
    if entry['published']:
        try:
            release_date = datetime.strptime(entry['published'][:-6], '%a, %d %b %Y %H:%M:%S')
        except ValueError:
            release_date = datetime.strptime(entry['published'][:-13], '%a, %d %b %Y')
    return formatDatetime_(release_date)


def newEpisodeList(feed, podcast_id, now):
    episodes = []
    formated_now = formatDatetime_(now)
    for entry in feed['entries']:
        name = entry['title'].encode('ascii', 'ignore')
        exisiting_episodes = db.Episode.query.filter_by(name=name, podcast_id=podcast_id)
        if exisiting_episodes.count() > 0:
            continue
        data = {}
        data['audio_link'] = GetAudioLink(entry['links'], 'audio/mpeg')
        data['release_date'] = GetEpisodeRelease(entry, now)
        data['created'] = formated_now
        data['name'] = name
        data['length'] = GetSecondDurationString(entry)
        data['last_updated'] = formated_now
        data['created'] = formated_now
        data['description'] = entry['description']
        data['podcast_id'] = podcast_id
        # TODO(dgriff): Add episode number, and size
        episode = db.CreateEpisode(data)
        episodes.append(episode)
    return episodes


def updatePodcast(rss):
    feed = feedparser.parse(rss)
    now = datetime.now()
    exisiting_podcasts = db.Podcast.query.filter_by(rss=rss)
    podcast_id = 0
    if exisiting_podcasts.count() > 0:
        podcast = exisiting_podcasts.first()
        podcast_id = podcast.id
        podcast.last_fetched = formatDatetime_(now)
        db.Update(podcast)
    else:
        podcast_id = createNewPodcast(rss, feed, now)
    episodes = newEpisodeList(feed, podcast_id, now)


def createNewPodcast(rss, feed, now):
    feedObj = feed['feed']

    feed_links = [link for link in feedObj['links'] if link['type'] == 'text/html']
    website = feed_links[0]['href'] if len(feed_links) > 0 else ''
    formated_now = formatDatetime_(now)
    data = {}
    data['created'] = formated_now
    data['rss'] = rss
    data['website'] = website
    data['name'] = feedObj['title']
    data['last_fetched'] = formated_now
    data['last_updated'] = formated_now
    data['description'] = feedObj['description']
    if feedObj.get('publisher_detail'):
        data['producer'] = feedObj['publisher_detail'].get('name')
    elif feedObj.get('itunes_author'):
        data['producer'] = feedObj['itunes_author']
    img = getImage(feed)
    if img:
        data['image_link'] = img
    if feedObj.get('language'):
        data['language'] = feedObj['language']

    # TODO(dgriff): set network
    # TODO(dgriff): Add categories

    podcast = db.CreatePodcast(data)
    return podcast['id']

    

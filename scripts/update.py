from tools.feed_handler import updatePodcast
import db.model as db

def main():
    rss_list = [podcast.rss for podcast in db.Podcast.query.all()]
    for rss in rss_list:
        print "Updating podcast: {}".format(rss)
        updatePodcast(rss)

if __name__ == '__main__':
    main()
from flask import Flask
from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()
def create_app():
    app = Flask(__name__)
    app.config.from_pyfile('config.py')
    app.app_context().push()
    db.init_app(app)
    return app;

app = create_app()

def row_to_dict_(row):
    data = row.__dict__.copy()
    data['id'] = row.id
    data.pop('_sa_instance_state')
    return data


def CreatePodcast(data):
    with app.app_context():
        podcast = Podcast(**data)
        db.session.add(podcast)
        db.session.commit()
        return row_to_dict_(podcast)

def CreateEpisode(data):
    with app.app_context():
        episode = Episode(**data)
        db.session.add(episode)
        db.session.commit()
        return row_to_dict_(episode)

class Podcast(db.Model):
    __tablename__ = 'podcast'

    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(255))
    image_link = db.Column(db.String(255))
    description = db.Column(db.String(255))
    producer = db.Column(db.String(255))
    website = db.Column(db.String(255))
    network = db.Column(db.String(255))
    last_updated = db.Column(db.DateTime)
    created = db.Column(db.DateTime)
    last_fetched = db.Column(db.DateTime)
    rss = db.Column(db.String(255))

    def __repr__(self):
        return "{Podcast(name='%s', id='%d, last_updated=%s, rss=%s}".format(self.name, self.id, self.last_updated, self.rss)

class Episode(db.Model):
    __tablename__ = 'episode'
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(255))
    release_date = db.Column(db.DateTime)
    length = db.Column(db.Integer)
    size = db.Column(db.Integer)
    description = db.Column(db.String(255))
    audio_link = db.Column(db.String(255))
    last_updated = db.Column(db.DateTime)
    created = db.Column(db.DateTime)
    podcast = db.Column(db.ForeignKey('podcast.id'))
    episode_number = db.Column(db.Integer)
    created_timestamp = db.Column(db.DateTime)

    def __repr__(self):
        return "{Episode(podcast_id='%s', name='%s', id='%d', length=%s, url=%s)}".format(self.podcast.id, self.name, self.id, self.length, self.audio_link)


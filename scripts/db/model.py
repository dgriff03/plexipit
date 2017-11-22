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

def ObjecttoDict(row):
    data = row.__dict__.copy()
    data['id'] = row.id
    data.pop('_sa_instance_state')
    return data


def CreatePodcast(data):
    with app.app_context():
        podcast = Podcast(**data)
        db.session.add(podcast)
        db.session.commit()
        return ObjecttoDict(podcast)

def CreateEpisode(data):
    with app.app_context():
        episode = Episode(**data)
        db.session.add(episode)
        db.session.commit()
        return ObjecttoDict(episode)

class Podcast(db.Model):
    __tablename__ = 'podcast'

    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(255))
    image_link = db.Column(db.String(511))
    description = db.Column(db.Text)
    producer = db.Column(db.String(255))
    website = db.Column(db.String(511))
    network = db.Column(db.String(255))
    last_updated = db.Column(db.DateTime)
    created = db.Column(db.DateTime)
    last_fetched = db.Column(db.DateTime)
    rss = db.Column(db.String(511))
    language = db.Column(db.String(255))

class Episode(db.Model):
    __tablename__ = 'episode'
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(255))
    release_date = db.Column(db.DateTime)
    length = db.Column(db.Integer)
    size = db.Column(db.Integer)
    description = db.Column(db.Text)
    audio_link = db.Column(db.String(511))
    last_updated = db.Column(db.DateTime)
    created = db.Column(db.DateTime)
    podcast_id = db.Column(db.ForeignKey('podcast.id'))
    episode_number = db.Column(db.Integer)
    created = db.Column(db.DateTime)

def Update(obj):
    with app.app_context():
        db.session.commit()
        return ObjecttoDict(obj)

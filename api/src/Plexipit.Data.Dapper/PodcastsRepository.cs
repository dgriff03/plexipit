using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Plexipit.Models.Models;

namespace Plexipit.Data.Dapper
{
    public class PodcastsRepository
    {
        private readonly string _connectionString;
        private const string FULL_PODCAST_SELECT = "id, name, image_link as imagelink, description, producer, website, network, created, last_updated as lastupdated, last_fetched as lastfetched, rss, language";
        private const string FULL_EPISODE_SELECT = "id, name, description, audio_link as audiolink, length, size, episode_number as episodenumber, created, release_date as releasedate, last_updated as lastupdated";


        public PodcastsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Podcast>> GetPodcasts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var podcasts = await conn.QueryAsync<Podcast>("select " + FULL_PODCAST_SELECT + " from podcast").ConfigureAwait(false);
                return podcasts.AsList();
            }
        }

        public async Task<Podcast> GetPodcast(long id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var podcast = await conn.QueryAsync<Podcast>("select " + FULL_PODCAST_SELECT + " from podcast where id = @id", new { id = id }).ConfigureAwait(false);
                return podcast.Single();
            }
        }

        public async Task<List<Episode>> GetEpisodes(long podcastId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var episodes = await conn.QueryAsync<Episode>("select " + FULL_EPISODE_SELECT + " from episode where podcast_id = @id", new { id = podcastId }).ConfigureAwait(false);
                return episodes.AsList();
            }
        }

        public async Task<Episode> GetEpisode(long id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var episode = await conn.QueryAsync<Episode>("select " + FULL_EPISODE_SELECT + " from episode id = @id", new { id = id }).ConfigureAwait(false);
                return episode.Single();
            }
        }
    }
}

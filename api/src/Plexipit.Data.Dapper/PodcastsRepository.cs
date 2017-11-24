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
        public string _connectionString;

        public PodcastsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Podcast>> GetPodcasts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var podcasts = await conn.QueryAsync<Podcast>("select * from podcast").ConfigureAwait(false);
                return podcasts.AsList();
            }
        }

        public async Task<Podcast> GetPodcast(long id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var podcast = await conn.QueryAsync<Podcast>("select * from podcast where id = @id", new { id = id }).ConfigureAwait(false);
                return podcast.Single();
            }
        }
    }
}

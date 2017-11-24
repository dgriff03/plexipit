using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Plexipit.Models.Models;

namespace Plexipit.Data.Dapper.Modules
{
    public class PodcastModule
    {
        private readonly string _connectionString;
        private const string FULL_PODCAST_SELECT = "id, name, image_link as imagelink, description, producer, website, network, created, last_updated as lastupdated, last_fetched as lastfetched, rss, language";

        public PodcastModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Podcast>> GetAll()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var podcasts = await conn.QueryAsync<Podcast>("select " + FULL_PODCAST_SELECT + " from podcast").ConfigureAwait(false);
                if (!podcasts.Any())
                {
                    return null; // TODO: set up an error
                }

                return podcasts.AsList();
            }
        }

        public async Task<Podcast> Get(long id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var podcast = await conn.QueryAsync<Podcast>("select " + FULL_PODCAST_SELECT + " from podcast where id = @id", new { id = id }).ConfigureAwait(false);
                if (!podcast.Any())
                {
                    return null; // TODO: set up an error
                }

                return podcast.Single();
            }
        }

        public async Task<List<Category>> GetCategories(long podcastId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var categories = await conn.QueryAsync<Category>("select distinct name from podcast_category pc left join category c on pc.category_id = c.id").ConfigureAwait(false);
                if (!categories.Any())
                {
                    return null; // TODO: set up an error
                }

                return categories.AsList();
            }
        }
    }
}

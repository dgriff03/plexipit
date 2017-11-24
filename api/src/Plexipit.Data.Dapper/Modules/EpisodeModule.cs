using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Plexipit.Models.Models;

namespace Plexipit.Data.Dapper.Modules
{
    public class EpisodeModule
    {
        private readonly string _connectionString;
        private const string FULL_EPISODE_SELECT = "id, name, description, audio_link as audiolink, length, size, episode_number as episodenumber, created, release_date as releasedate, last_updated as lastupdated";

        public EpisodeModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Episode>> GetAllById(long podcastId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var episodes = await conn.QueryAsync<Episode>("select " + FULL_EPISODE_SELECT + " from episode where podcast_id = @id", new { id = podcastId }).ConfigureAwait(false);
                if (!episodes.Any())
                {
                    return null; // TODO: set up an error
                }

                return episodes.AsList();
            }
        }

        public async Task<Episode> Get(long id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var episode = await conn.QueryAsync<Episode>("select " + FULL_EPISODE_SELECT + " from episode where id = @id", new { id = id }).ConfigureAwait(false);
                if (!episode.Any())
                {
                    return null; // TODO: set up an error
                }

                return episode.Single();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
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

        private MySqlConnection GetMySqlConnection(string connectionString, bool open = true)
        {
            var conn = new MySqlConnection(connectionString);
            if (open) conn.Open();

            return conn;
        }

        public List<Podcast> GetPodcasts()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plexipit.Models.Models
{
    public class Podcast
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string ImageLink { get; set; }
        public string Website { get; set; }
        public string Network { get; set; }
        public string Rss { get; set; }
        public string Language { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastFetched { get; set; }
        public List<Category> Categories { get; set; }
        public List<Episode> Episodes { get; set; }

        public Podcast()
        {
            Episodes = new List<Episode>();
            Categories = new List<Category>();
        }
    }
}

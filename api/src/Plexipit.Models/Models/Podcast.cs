using System.Collections.Generic;
            
namespace Plexipit.Models.Models
{
    public class Podcast
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string ImageLink { get; set; }
        public List<Episode> Episodes { get; set; }

        public Podcast()
        {
            Episodes = new List<Episode>();
        }
    }
}

using System;
namespace Plexipit.Models.Models
{
    public class Episode
    {
        public long Id { get; set; }
        public int EpisodeNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AudioLink { get; set; }
        public int Length { get; set; }
        public long Size { get; set; }
        public DateTime Created { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

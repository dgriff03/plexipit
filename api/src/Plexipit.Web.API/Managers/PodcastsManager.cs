using System.Collections.Generic;
using System.Threading.Tasks;
using Plexipit.Data.Dapper.Modules;
using Plexipit.Models.Models;

namespace Plexipit.Web.API.Managers
{
    public class PodcastsManager
    {
        private readonly PodcastModule _podcastModule;
        private readonly EpisodeModule _episodeModule;

        public PodcastsManager(PodcastModule podcastModule, EpisodeModule episodeModule)
        {
            _podcastModule = podcastModule;
            _episodeModule = episodeModule;
        }

        public async Task<List<Podcast>> GetPodcasts()
        {
            // TODO: Sorting?
            return await _podcastModule.GetAll().ConfigureAwait(false);
        }

        public async Task<Podcast> GetPodcast(long id)
        {
            var podcast = await _podcastModule.Get(id).ConfigureAwait(false);
            if (podcast != null)
            {
                podcast.Episodes = await _episodeModule.GetAllById(id).ConfigureAwait(false);
                podcast.Categories = await _podcastModule.GetCategories(id).ConfigureAwait(false);
            }

            return podcast; 
        }
    }
}

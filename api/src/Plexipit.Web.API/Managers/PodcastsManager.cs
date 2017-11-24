using System.Collections.Generic;
using System.Threading.Tasks;
using Plexipit.Data.Dapper;
using Plexipit.Models.Models;

namespace Plexipit.Web.API.Managers
{
    public class PodcastsManager
    {
        private PodcastsRepository _podcastRepository;

        public PodcastsManager(PodcastsRepository podcastsRepository)
        {
            _podcastRepository = podcastsRepository;
        }

        public async Task<List<Podcast>> GetPodcasts()
        {
            // TODO: Sorting?
            return await _podcastRepository.GetPodcasts().ConfigureAwait(false);
        }

        public async Task<Podcast> GetPodcast(long id)
        {
            var podcast = await _podcastRepository.GetPodcast(id).ConfigureAwait(false);
            if (podcast != null)
            {
                podcast.Episodes = await _podcastRepository.GetEpisodes(id).ConfigureAwait(false);
            }

            return podcast; 
        }
    }
}

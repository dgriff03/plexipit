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
            // TODO: map episodes in
            return await _podcastRepository.GetPodcast(id).ConfigureAwait(false);
        }
    }
}

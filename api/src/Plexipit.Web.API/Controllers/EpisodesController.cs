using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plexipit.Web.API.Managers;

namespace Plexipit.Web.API.Controllers
{
    [Route("api/v1/podcasts/{podcastId}/episodes")]
    public class EpisodesController : ControllerBase
    {
        private readonly PodcastsManager _podcastsManager;

        public EpisodesController(PodcastsManager podcastsManager)
        {
            _podcastsManager = podcastsManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(long podcastId)
        {
            var result = await _podcastsManager.GetPodcastEpisodes(podcastId);
            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(long podcastId, long id)
        {
            var result = await _podcastsManager.GetPodcastEpisode(podcastId, id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

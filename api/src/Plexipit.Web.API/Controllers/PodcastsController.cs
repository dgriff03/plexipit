using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plexipit.Web.API.Managers;

namespace Plexipit.Web.API.Controllers
{
    [Route("api/v1/podcasts")]
    public class PodcastsController : Controller
    {
        private PodcastsManager _podcastsManager;

        public PodcastsController(PodcastsManager podcastsManager)
        {
            _podcastsManager = podcastsManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _podcastsManager.GetPodcasts();
            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _podcastsManager.GetPodcast(id);
            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plexipit.Web.API.Managers;

namespace Plexipit.Web.API.Controllers
{
    [Route("api/v1/podcasts")]
    public class PodcastsController : ControllerBase
    {
        private readonly PodcastsManager _podcastsManager;

        public PodcastsController(PodcastsManager podcastsManager)
        {
            _podcastsManager = podcastsManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _podcastsManager.GetPodcasts();
            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _podcastsManager.GetPodcast(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

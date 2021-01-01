using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusic.API.Mapping;
using MyMusic.API.Ressources;
using MyMusic.Core.Models;
using MyMusic.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicServices _musicServices;
        private readonly IMapper _mapperService;
        public MusicController(IMusicServices musicServices, IMapper mapperService)
        {
            _musicServices = musicServices;
            _mapperService = mapperService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MusicRessource>>> GetAllMusic()
        {
            try
            {
                var musics = await _musicServices.GetAllWithArtist();
                var musicRessources = _mapperService.Map<IEnumerable<Music>, IEnumerable<MusicRessource>>(musics);
                return Ok(musicRessources);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }   
       
        }
    }
}

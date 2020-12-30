using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public MusicController(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Music>>> GetAllMusic()
        {
            try
            {
                var musics = await _musicServices.GetAllWithArtist();
                return Ok(musics);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
       
        }
    }
}

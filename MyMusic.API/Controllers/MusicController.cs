using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusic.API.Mapping;
using MyMusic.API.Ressources;
using MyMusic.API.Validation;
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
        private readonly IArtistService _artistService;
        private readonly IComposerService _composerService;
            

        private readonly IMapper _mapperService;
        public MusicController(IMusicServices musicServices, IMapper mapperService, IArtistService artistService)
        {
            _musicServices = musicServices;
            _artistService = artistService;
            _mapperService = mapperService;
        }

        //======GetALLMusic ========

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

        //========GetMusicByID========

        [HttpGet("{id}")]
        public async Task< ActionResult< MusicRessource>> GetMusicByID(int id)
        {

            try
            {

                var music = await _musicServices.GetMusicById(id);
                if (music == null) return NotFound();
                var musicRessource = _mapperService.Map<Music, MusicRessource>(music);
                return Ok(musicRessource);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

       
        }

        //===========CreatMusic===================

        [HttpPost("")]
        public async Task<ActionResult <MusicRessource>> CreateMusic(SaveMusicRessource saveMusicRessource)
        {
            try
            {
                //======Validation =====
                var validation = new SaveMusicRessourceValidator();
                var validationResult = await validation.ValidateAsync(saveMusicRessource);
                if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
                // mappage
                var music = _mapperService.Map<SaveMusicRessource, Music>(saveMusicRessource);
                // Creation Music
                var newMusic = await _musicServices.CreateMusic(music);
                // mappage
                var musicRessources = _mapperService.Map<Music, MusicRessource>(newMusic);

                return Ok(musicRessources);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // ======Update Music==================

        [HttpPut("{id}")]

        public async Task<ActionResult<MusicRessource>> UpdateMusic (int id, SaveMusicRessource saveMusicRessource)
        {
            // validation
            var validation = new SaveMusicRessourceValidator();
            var validationResult = await validation.ValidateAsync(saveMusicRessource);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            // Verifier existance de l'objet Music
            var music=  await _musicServices.GetMusicById(id);
            if (music == null) return BadRequest("Music n'existe pas");
            //mappage 
            var musicUpdate = _mapperService.Map<SaveMusicRessource, Music>(saveMusicRessource);
            // update Music
             await _musicServices.UpdateMusic(music, musicUpdate);
            //===mappageNewMusicRessource
            var musicUpdateNew = await _musicServices.GetMusicById(id);
            var musicUpdateNewRessource = _mapperService.Map<Music, MusicRessource>(musicUpdateNew);
            return Ok(musicUpdateNewRessource);
        }


        // =======Delete Music=========

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMusic(int id)
        {
            var music = await _musicServices.GetMusicById(id);
            if (music == null) return BadRequest("music n'existe pas");
            await _musicServices.DeleteMusic(music);
            return NoContent();
        }

        //======= Recupère les Music de L'artist By ID=======

        [HttpGet("Artist/id")]
        public  async Task<ActionResult <IEnumerable<MusicRessource>>> GetALlMusicByArtistID(int id)
        {
            var artist =  await _artistService.GetArtistById(id);
            if (artist == null) return BadRequest("la artiste n'existe pas");
            var musics = await _musicServices.GetMusicsByArtistId(id);
            if (musics == null) return BadRequest("Les musics n'existe pas");
            // mappage 
            var musicRessources = _mapperService.Map<IEnumerable<Music>, IEnumerable<MusicRessource>>(musics);
            return Ok(musicRessources);


        }

        // =====Delete Artist=======

        

    }

}

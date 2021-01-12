using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapperService;

        public ArtistController( IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapperService = mapper;
        }
        /// <summary>
        /// Recupere l'ensemble des Artiste
        /// </summary>
        /// <returns></returns>

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistRessource>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();
            //=======Mappage Artist====
            var artistRessources= _mapperService.Map<IEnumerable<Artist>, IEnumerable<ArtistRessource>>(artists);
            return Ok(artistRessources);
        }

        //======GetArtistByID ========
        [HttpGet("{id}")]
        public async Task<ActionResult <ArtistRessource>> GetArtistById(int id)
        {
            try
            {
                var artist =  await _artistService.GetArtistById(id);
                var artistRessource = _mapperService.Map<Artist, ArtistRessource>(artist);
                return Ok(artistRessource);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //======CreationArtiste======

        [HttpPost("")]
        public async Task<ActionResult <ArtistRessource>> CreateArtist(SaveArtistRessource saveArtistRessource)
        {
            try
            {
                var validation = new SaveArtistRessourceValidator();
                var validationResult = await validation.ValidateAsync(saveArtistRessource);
                if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
                //=== mappage artiste
                var artist = _mapperService.Map<SaveArtistRessource, Artist>(saveArtistRessource);
                //====Creation New Artist====
                 var artistNew= await _artistService.CreateArtist(artist);
                // ========Mappage Artiste sur ArtisteRessource===
                var artisteRessource = _mapperService.Map<Artist, ArtistRessource>(artistNew);
                return Ok(artisteRessource);

            }
            catch (Exception ex) 
            {

                return BadRequest(ex.Message);
            }

        }

        //=====Update Artiste=======
        [HttpPut("")]

        public async Task<ActionResult<ArtistRessource>> UpdateArtistById(int id,SaveArtistRessource saveArtistRessource)
        {
            try
            {
                var validation = new SaveArtistRessourceValidator();
                var validationResult = await validation.ValidateAsync(saveArtistRessource);
                if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
                //======= verif Artist======
                var artist =  await _artistService.GetArtistById(id);
                if (artist == null) return BadRequest(" artist n'existe pas !");
                // ========Mappage Sur noveau Artiste ====
                var artistModif = _mapperService.Map<SaveArtistRessource, Artist>(saveArtistRessource);
                // ======Update Artiste ======
                await _artistService.UpdateArtist(artist, artistModif);

                //======Get New Artist ======
                var artistNew = await _artistService.GetArtistById(id);
                // ====Mappage ======
                var artisteRessource = _mapperService.Map<Artist, ArtistRessource>(artistNew);

                return Ok(artisteRessource);
            
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //======ArtistDelete======
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletArtistById(int id)
        {
            try
            {
                var artist =  await _artistService.GetArtistById(id);
                if (artist == null) return BadRequest("l'artist est null");
                await _artistService.DeleteArtist(artist);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }





    }
}

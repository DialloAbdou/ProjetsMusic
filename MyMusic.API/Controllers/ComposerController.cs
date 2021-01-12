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
    public class ComposerController : ControllerBase
    {

        private readonly IMapper _mapperService;
        private readonly IComposerService _composerService;

        public ComposerController( IComposerService composerService, IMapper mapper)
        {
            _mapperService = mapper;
            _composerService = composerService;
        } 

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ComposerRessource>>> GetAllComposer()
        {
            var composers = await _composerService.GetAllComposers();
            // =======Mappage====
            var composerRessources = _mapperService.Map<IEnumerable<Composer>, IEnumerable<ComposerRessource>>(composers);
            return Ok(composerRessources);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ComposerRessource>> GetComposerById(string id)
        {
            var composer = await _composerService.GetComposerById(id);
            //======Mapper Donnée======
           var composerRessource = _mapperService.Map<Composer, ComposerRessource>(composer);

            return Ok(composerRessource);


        }
        [HttpPost("")]
        public async Task<ActionResult<ComposerRessource>> CreateComposer(SaveComposerRessource saveComposerRessource)
        {
            var validation = new SaveComposerRessourceValidator();
            var valiationResutl = await validation.ValidateAsync(saveComposerRessource);
            if (!valiationResutl.IsValid) return BadRequest("Donnée non valide");
            // ======Mappage des données===
            var composer = _mapperService.Map<SaveComposerRessource, Composer>(saveComposerRessource);
            var conposernew = await _composerService.Create(composer);
            // ======Mappage des Données =====
            var composerRessource = _mapperService.Map<Composer, ComposerRessource>(conposernew);

            return Ok(composerRessource);

        }
        [HttpPut("")]
        public async Task<ActionResult<ComposerRessource>> UpdateComposer(string id, SaveComposerRessource saveComposerRessource)
        {
            var validation = new SaveComposerRessourceValidator();
            var valiationResutl = await validation.ValidateAsync(saveComposerRessource);
            if (!valiationResutl.IsValid) return BadRequest("Donnée non valide");
            //======= GetComposer ====
            var composer = await  _composerService.GetComposerById(id);
            if (composer == null) return BadRequest("composer n'existe pas");
            var composerModif = _mapperService.Map<SaveComposerRessource, Composer>(saveComposerRessource);
            //======Update======
            _composerService.update(id, composerModif);
            var composerNew =  await _composerService.GetComposerById(id);
            var composerRessource = _mapperService.Map<Composer, ComposerRessource>(composerNew);

            return Ok(composerRessource);

        }


        //======ArtistDelete======
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletComposerById(string id)
        {
            try
            {
                var composer = await _composerService.GetComposerById(id);
                if (composer == null) return BadRequest("l'artist est null");
                await _composerService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }







    }
}

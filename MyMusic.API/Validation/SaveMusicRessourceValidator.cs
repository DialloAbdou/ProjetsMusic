using FluentValidation;
using MyMusic.API.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Validation
{
    public class SaveMusicRessourceValidator : AbstractValidator<SaveMusicRessource>
    {
        public SaveMusicRessourceValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.ArtistId)
                .NotEmpty()
                .WithMessage("'ArtistId', doit etre diffrent 0.");
                
        } 

    }
}

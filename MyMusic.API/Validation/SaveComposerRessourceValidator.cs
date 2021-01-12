using FluentValidation;
using MyMusic.API.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Validation
{
    public class SaveComposerRessourceValidator : AbstractValidator<SaveComposerRessource>
    {
        public SaveComposerRessourceValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.LastName)
               .NotEmpty()
               .MaximumLength(50);
        }
    }
        
    
}

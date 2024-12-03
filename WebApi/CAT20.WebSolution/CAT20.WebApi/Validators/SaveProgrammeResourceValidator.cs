using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveProgrammeResourceValidator : AbstractValidator<SaveProgrammeResource>
    {
        public SaveProgrammeResourceValidator()
        {
            RuleFor(a => a.NameEnglish)
                .NotEmpty()
                .WithMessage("The Name English is longer than allowed")
                .MaximumLength(100);
            RuleFor(a => a.Code)
                .NotEmpty()
                .WithMessage("The Code is Required.");
        }
    }
}

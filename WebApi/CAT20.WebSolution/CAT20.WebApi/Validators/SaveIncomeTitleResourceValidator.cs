using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveIncomeTitleResourceValidator : AbstractValidator<SaveIncomeTitleResource>
    {
        public SaveIncomeTitleResourceValidator()
        {
            RuleFor(a => a.NameEnglish)
                .NotEmpty()
                .WithMessage("The Name English is longer than allowed")
                .MaximumLength(100);
        }
    }
}

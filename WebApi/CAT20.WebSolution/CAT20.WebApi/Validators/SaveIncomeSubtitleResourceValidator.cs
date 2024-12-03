using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveIncomeSubtitleResourceValidator : AbstractValidator<SaveIncomeSubtitleResource>
    {
        public SaveIncomeSubtitleResourceValidator()
        {
            RuleFor(a => a.NameEnglish)
                .NotEmpty()
                .WithMessage("The Name English is longer than allowed")
                .MaximumLength(100);
        }
    }
}

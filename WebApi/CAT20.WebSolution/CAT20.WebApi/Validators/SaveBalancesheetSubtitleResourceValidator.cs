using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveBalancesheetSubtitleResourceValidator : AbstractValidator<SaveBalancesheetSubtitleResource>
    {
        public SaveBalancesheetSubtitleResourceValidator()
        {
            RuleFor(a => a.NameEnglish)
                .NotEmpty()
                .WithMessage("Name English Cannot Be Empty");
        }
    }
}

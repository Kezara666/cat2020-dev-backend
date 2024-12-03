using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveBalancesheetTitleResourceValidator : AbstractValidator<SaveBalancesheetTitleResource>
    {
        public SaveBalancesheetTitleResourceValidator()
        {
            RuleFor(a => a.NameEnglish)
                .NotEmpty()
                .WithMessage("The Name English Cannot Be empty");
        }
    }
}

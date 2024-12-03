using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.User.Save;

namespace CAT20.Api.Validators
{
    public class SaveUserHasPreviledgeResourceValidator : AbstractValidator<SaveUserHasPreviledgeResource>
    {
        public SaveUserHasPreviledgeResourceValidator()
        {
            //RuleFor(a => a.)
            //    .NotEmpty()
            //    .WithMessage("The Name English is longer than allowed")
            //    .MaximumLength(100);
        }
    }
}

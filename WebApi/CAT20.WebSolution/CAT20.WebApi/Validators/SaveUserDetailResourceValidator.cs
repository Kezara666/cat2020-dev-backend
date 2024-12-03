using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.User.Save;

namespace CAT20.Api.Validators
{
    public class SaveUserDetailResourceValidator : AbstractValidator<SaveUserDetailResource>
    {
        public SaveUserDetailResourceValidator()
        {
            RuleFor(a => a.Username)
                .NotEmpty()
                .WithMessage("The Username is longer than allowed")
                .MaximumLength(100);
        }
    }
}

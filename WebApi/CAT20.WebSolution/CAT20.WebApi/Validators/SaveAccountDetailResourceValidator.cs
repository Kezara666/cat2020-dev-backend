using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveAccountDetailResourceValidator : AbstractValidator<SaveAccountDetailResource>
    {
        public SaveAccountDetailResourceValidator()
        {
            RuleFor(a => a.AccountNo)
                .NotEmpty()
                .WithMessage("The Account Number cannot empty");
        }
    }
}

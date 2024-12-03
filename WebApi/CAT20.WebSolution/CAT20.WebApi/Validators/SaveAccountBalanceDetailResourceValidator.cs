using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveAccountBalanceDetailResourceValidator : AbstractValidator<SaveAccountBalanceDetailResource>
    {
        public SaveAccountBalanceDetailResourceValidator()
        {
            RuleFor(a => a.BalanceAmount)
                .NotEmpty()
                .WithMessage("Balance Amount Cannot empty");
        }
    }
}

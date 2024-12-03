using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveBalancesheetBalanceResourceValidator : AbstractValidator<SaveBalancesheetBalanceResource>
    {
        public SaveBalancesheetBalanceResourceValidator()
        {
            RuleFor(a => a.Balance)
                .NotEmpty()
                .WithMessage("Balance Cannot Be Empty");
        }
    }
}

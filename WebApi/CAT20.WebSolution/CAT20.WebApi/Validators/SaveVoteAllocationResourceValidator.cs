using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.Vote.Save;

namespace CAT20.Api.Validators
{
    public class SaveVoteAllocationResourceValidator : AbstractValidator<SaveVoteAllocationResource>
    {
        public SaveVoteAllocationResourceValidator()
        {
            RuleFor(a => a.AllocationAmount)
                .NotEmpty()
                .WithMessage("Allocation Amouunt Cannot be empty");
        }
    }
}

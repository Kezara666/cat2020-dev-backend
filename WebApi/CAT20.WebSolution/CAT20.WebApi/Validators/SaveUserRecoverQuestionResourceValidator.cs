using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAT20.WebApi.Resources.User.Save;

namespace CAT20.Api.Validators
{
    public class SaveUserRecoverQuestionResourceValidator : AbstractValidator<SaveUserRecoverQuestionResource>
    {
        public SaveUserRecoverQuestionResourceValidator()
        {
            RuleFor(a => a.Question)
                .NotEmpty()
                .WithMessage("The Question is longer than allowed")
                .MaximumLength(100);
        }
    }
}

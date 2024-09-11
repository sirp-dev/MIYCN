using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DialyActivityValidator : AbstractValidator<DialyActivity>
    {
        public DialyActivityValidator()
        {
            RuleFor(data => data.Date).NotEmpty().WithMessage("Date cannot be empty.");

        }
    }
}

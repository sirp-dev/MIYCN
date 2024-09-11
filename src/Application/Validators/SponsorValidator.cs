using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
      public class SponsorValidator : AbstractValidator<Sponsor>
    {
        public SponsorValidator()
        {
            RuleFor(data => data.Name).NotEmpty().WithMessage("Name cannot be empty.");

        }
    }
}

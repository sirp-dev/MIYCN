using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
      public class TrainingParticipantValidator : AbstractValidator<TrainingParticipant>
    {
        public TrainingParticipantValidator()
        {
            RuleFor(data => data.UserId).NotEmpty().WithMessage("UserId cannot be empty.");

        }
    }
}

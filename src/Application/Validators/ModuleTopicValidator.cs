﻿using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
     public class ModuleTopicValidator : AbstractValidator<ModuleTopic>
    {
        public ModuleTopicValidator()
        {
            RuleFor(data => data.Title).NotEmpty().WithMessage("Title cannot be empty.");

        }
    }
}

using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
      public class TestCategoryValidator : AbstractValidator<TestCategory>
    {
        public TestCategoryValidator()
        {
            RuleFor(data => data.Title).NotEmpty().WithMessage("Title cannot be empty.");

        }
    }
}

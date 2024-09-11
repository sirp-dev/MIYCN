using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    //public class MovieValidator : AbstractValidator<Movie>
    //{
    //    public MovieValidator()
    //    {
    //        RuleFor(movie => movie.Title).NotEmpty().WithMessage("Title cannot be empty.");
    //        RuleFor(movie => movie.Title).MaximumLength(255).WithMessage("Title must be less than 255 characters.");

    //        RuleFor(movie => movie.Year).MaximumLength(4).WithMessage("Year must be 4 characters or less.");

    //        RuleFor(movie => movie.ImdbId).MaximumLength(255).WithMessage("IMDB ID must be less than 255 characters.");

    //        RuleFor(movie => movie.PosterUrl).MaximumLength(255).WithMessage("Poster URL must be less than 255 characters.");

          
    //    }
    //}
}

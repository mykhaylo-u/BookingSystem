using BookingSystem.Api.ViewModels.Movie;
using BookingSystem.Api.ViewModels.Movie.Requests;
using BookingSystem.Utilities;
using FluentValidation;
using System.Globalization;

namespace BookingSystem.Api.Validators.Movie
{
    public class MovieValidator : AbstractValidator<AddMovieRequest>
    {
        public MovieValidator()
        {
            RuleFor(x => x.ShowStartDate)
                .NotEmpty()
                .WithMessage("ShowStartDate is required.")
                .Custom((x, context) =>
                {
                    if (string.IsNullOrEmpty(x) || !DateTime.TryParseExact(x, Constants.DefaultDateFormat,
                            CultureInfo.InvariantCulture, DateTimeStyles.None,
                            out var dt))
                    {
                        context.AddFailure("ShowStartDate is should be in format YYYY-MM-dd.");
                    }
                });
        }
    }
}

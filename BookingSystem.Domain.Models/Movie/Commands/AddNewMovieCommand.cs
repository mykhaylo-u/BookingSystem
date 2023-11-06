using MediatR;

namespace BookingSystem.Domain.Models.Movie.Commands
{
    public class AddNewMovieCommand : IRequest<Movie>
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public string Summary { get; set; }
        public DateTime ShowStartDate { get; set; }
        public DateTime ShowEndDate { get; set; }

        public AddNewMovieCommand(string title, TimeSpan duration, string genre, string summary, DateTime showStartDate,
            DateTime showEndDate)
        {
            Title = title;
            Duration = duration;
            Genre = genre;
            Summary = summary;
            ShowStartDate = showStartDate;
            ShowEndDate = showEndDate;
        }
    }
}

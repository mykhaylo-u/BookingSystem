using MediatR;

namespace BookingSystem.Domain.Models.Movie.Commands
{
    public class UpdateMovieCommand : IRequest<Movie>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public string Summary { get; set; }
        public DateTime ShowStartDate { get; set; }
        public DateTime ShowEndDate { get; set; }

        public UpdateMovieCommand(int id, string title, TimeSpan duration, string genre, string summary, DateTime showStartDate,
            DateTime showEndDate)
        {
            Id = id;
            Title = title;
            Duration = duration;
            Genre = genre;
            Summary = summary;
            ShowStartDate = showStartDate;
            ShowEndDate = showEndDate;
        }
    }
}

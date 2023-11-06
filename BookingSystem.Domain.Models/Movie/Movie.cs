namespace BookingSystem.Domain.Models.Movie
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public string Summary { get; set; }
        public DateTime ShowStartDate { get; set; }
        public DateTime ShowEndDate { get; set; }

        public Movie(string title, TimeSpan duration, string genre, DateTime showStartDate, DateTime showEndDate, string summary)
        {
            Title = title;
            Duration = duration;
            Genre = genre;
            ShowStartDate = showStartDate;
            ShowEndDate = showEndDate;
            Summary = summary;
        }
    }
}

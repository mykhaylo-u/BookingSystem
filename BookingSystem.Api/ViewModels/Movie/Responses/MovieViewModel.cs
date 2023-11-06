namespace BookingSystem.Api.ViewModels.Movie.Responses
{
    /// <summary>
    /// A ViewModel representing a movie.
    /// </summary>
    public class MovieViewModel
    {
        /// <summary>
        /// The identifier for the movie.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// The title of the movie.
        /// </summary>
        /// <example>HomeMovie</example>
        public string Title { get; set; }

        /// <summary>
        /// The duration of the movie.
        /// </summary>
        /// <example>2h 10m</example>
        public string Duration { get; set; }

        /// <summary>
        /// The genre of the movie.
        /// </summary>
        /// <example>Comedy</example>
        public string Genre { get; set; }

        /// <summary>
        /// The summary of the movie.
        /// </summary>
        /// <example>Super long summary</example>
        public string Summary { get; set; }

        /// <summary>
        /// The start date for the movie.
        /// </summary>
        /// <example>2023-11-10</example>
        public string ShowStartDate { get; set; }

        /// <summary>
        /// The end date for the movie.
        /// </summary>
        /// <example>2023-11-24</example>
        public string ShowEndDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Api.ViewModels.Movie.Requests
{
    /// <summary>
    /// Represents a request to create a new movie.
    /// </summary>
    public class AddMovieRequest
    {
        /// <summary>
        /// Movie title.
        /// </summary>
        /// <example>SomeVideo</example>
        [Required(ErrorMessage = "Title should not bew empty")]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Movie duration in minutes.
        /// </summary>
        /// <example>180</example>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        /// Movie genre.
        /// </summary>
        /// <example>Comedy</example>
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }

        /// <summary>
        /// Movie summary.
        /// </summary>
        /// <example>Some super long summary.</example>
        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }

        /// <summary>
        /// Start date of the show.
        /// </summary>
        /// <example>2023-11-25</example>
        [Required]
        public string ShowStartDate { get; set; }

        /// <summary>
        /// End date of the show.
        /// </summary>
        /// <example>2023-11-29</example>
        [Required]
        public string ShowEndDate { get; set; }
    }
}

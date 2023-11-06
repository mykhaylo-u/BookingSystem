using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Api.ViewModels.Movie.Requests
{
    /// <summary>
    /// Represents a request to update movie.
    /// </summary>
    public class UpdateMovieRequest
    {
        /// <summary>
        /// Movie title.
        /// </summary>
        /// <example>SomeVideoUpdated</example>
        [Required(ErrorMessage = "Title should not bew empty")]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Movie duration in minutes.
        /// </summary>
        /// <example>160</example>
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
        /// <example>Some super long summary updated.</example>
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

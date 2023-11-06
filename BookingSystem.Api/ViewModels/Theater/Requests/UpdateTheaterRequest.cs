
namespace BookingSystem.Api.ViewModels.Theater.Requests
{
    /// <summary>
    /// Represents a request to create a new theater.
    /// </summary>
    public class UpdateTheaterRequest
    {
        /// <summary>
        /// Theater name.
        /// </summary>
        /// <example>Theater 1</example>
        public string Name { get; set; }

        /// <summary>
        /// Theater total number of seats.
        /// </summary>
        /// <example>25</example>
        public int TotalSeats { get; set; }
    }
}

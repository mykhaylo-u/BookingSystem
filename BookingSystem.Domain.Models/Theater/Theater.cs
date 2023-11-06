using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.Models.Theater
{
    public class Theater
    {
        /// <summary>
        /// ID of the theater.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name of the theater.
        /// </summary>
        /// <example>hall 1</example>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// capacity of the theater.
        /// </summary>
        /// <example>200</example>
        [Required]
        public int TotalSeats { get; set; }

        public Theater(string name, int totalSeats)
        {
            Name = name;
            TotalSeats = totalSeats;
        }
    }
}

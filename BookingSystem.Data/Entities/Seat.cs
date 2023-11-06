using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.Entities
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [ForeignKey("ShowTime")]
        public int ShowTimeId { get; set; }

        public virtual ShowTime ShowTime { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Data.Entities
{
    public class ShowTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Required]
        [ForeignKey("Theater")]
        public int TheaterId { get; set; }
        public virtual Theater Theater { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TicketPrice { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}

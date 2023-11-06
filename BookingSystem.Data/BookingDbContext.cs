using System.Reflection;
using BookingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        { }

        #region DbSets
        public DbSet<Entities.Movie> Movies { get; set; }
        public DbSet<Entities.Theater> Theaters { get; set; }
        public DbSet<Entities.ShowTime> ShowTimes { get; set; }
        public DbSet<Entities.Seat> Seats { get; set; }
        public DbSet<Entities.SeatReservation> SeatReservations { get; set; }
        public DbSet<Entities.BookingConfirmation> BookingConfirmations { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Movie>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Entities.Theater>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Entities.Seat>()
                .HasKey(x => new { x.Id, x.ShowTimeId });

            modelBuilder.Entity<Entities.ShowTime>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Entities.ShowTime>()
                .HasMany(t => t.Seats)
                .WithOne()
                .HasForeignKey(s => s.ShowTimeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShowTime>()
                .HasOne(st => st.Movie)
                .WithMany()
                .HasForeignKey(st => st.MovieId);

            modelBuilder.Entity<ShowTime>()
                .HasOne(st => st.Theater)
                .WithMany()
                .HasForeignKey(st => st.TheaterId);

            modelBuilder.Entity<Entities.SeatReservation>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<Entities.SeatReservation>()
                .HasMany(t => t.ReservedSeats)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entities.BookingConfirmation>()
                .HasKey(x => x.Id);


            modelBuilder.Entity<Entities.BookingConfirmation>()
                .HasOne(t => t.SeatReservation)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

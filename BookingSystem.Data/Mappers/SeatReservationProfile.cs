using AutoMapper;
using BookingSystem.Data.Entities;

namespace BookingSystem.Data.Mappers
{
    public class SeatReservationProfile : Profile
    {
        public SeatReservationProfile()
        {
            CreateMap<SeatReservation, Domain.Models.SeatReservation.SeatReservation>()
                .ReverseMap();

            CreateMap<BookingConfirmation, Domain.Models.SeatReservation.BookingConfirmation>()
                .ReverseMap();

        }
    }
}

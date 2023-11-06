using AutoMapper;
using BookingSystem.Data.Entities;

namespace BookingSystem.Data.Mappers
{
    public class SeatProfile : Profile
    {
        public SeatProfile()
        {
            CreateMap<Seat, Domain.Models.SeatReservation.Seat>().ReverseMap();
        }
    }
}

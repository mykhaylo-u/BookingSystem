using AutoMapper;
using BookingSystem.Api.ViewModels.Theater.Responses;
using BookingSystem.Domain.Models.SeatReservation;

namespace BookingSystem.Api.ViewModels.Theater.Mappers
{
    public class ThaterProfile : Profile
    {
        public ThaterProfile()
        {
            CreateMap<Domain.Models.Theater.Theater, TheaterViewModel>();

            CreateMap<Seat, SeatViewModel>();
        }
    }
}

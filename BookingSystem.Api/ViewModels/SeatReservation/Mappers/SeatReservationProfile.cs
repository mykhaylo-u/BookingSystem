using AutoMapper;
using BookingSystem.Api.ViewModels.SeatReservation.Responses;

namespace BookingSystem.Api.ViewModels.SeatReservation.Mappers
{
    public class SeatReservationProfile : Profile
    {
        public SeatReservationProfile()
        {
            CreateMap<Domain.Models.SeatReservation.SeatReservation, SeatReservationViewModel>();
            CreateMap<Domain.Models.SeatReservation.Seat, SeatViewModel>();
        }
    }
}

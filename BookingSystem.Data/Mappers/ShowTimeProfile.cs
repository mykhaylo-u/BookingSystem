using AutoMapper;
using BookingSystem.Data.Entities;

namespace BookingSystem.Data.Mappers
{
    public class ShowTimeProfile : Profile
    {
        public ShowTimeProfile()
        {
            CreateMap<ShowTime, Domain.Models.Showtime.ShowTime>()
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats))
                .ReverseMap();
        }
    }
}

using AutoMapper;
using BookingSystem.Data.Entities;

namespace BookingSystem.Data.Mappers
{
    public class TheaterProfile : Profile
    {
        public TheaterProfile()
        {
            CreateMap<Theater, Domain.Models.Theater.Theater>()
                //.ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats))
                .ReverseMap();
        }
    }
}

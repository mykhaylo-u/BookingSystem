using AutoMapper;

namespace BookingSystem.Data.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Entities.Movie, Domain.Models.Movie.Movie>().ReverseMap();
        }
    }
}

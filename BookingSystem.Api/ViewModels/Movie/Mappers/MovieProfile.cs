using AutoMapper;
using BookingSystem.Api.ViewModels.Movie.Responses;
using BookingSystem.Utilities;

namespace BookingSystem.Api.ViewModels.Movie.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Domain.Models.Movie.Movie, MovieViewModel>()
                .ForMember(d => d.Duration,
                    opt => opt.MapFrom(s => $"{s.Duration.Hours}h {s.Duration.Minutes}m"))
                .ForMember(d => d.ShowStartDate,
                    opt => opt.MapFrom(s => s.ShowStartDate.ToString(Constants.DefaultDateFormat)))
                .ForMember(d => d.ShowEndDate,
                    opt => opt.MapFrom(s => s.ShowEndDate.ToString(Constants.DefaultDateFormat)));

        }
    }
}

using AutoMapper;
using BookingSystem.Utilities;

namespace BookingSystem.Api.ViewModels.ShowTime.Mappers
{
    public class ShowTimeProfile : Profile
    {
        public ShowTimeProfile()
        {
            CreateMap<Domain.Models.Showtime.ShowTime, ShowTimeViewModel>()
                .ForMember(d => d.StartDateTime,
                    opt => opt.MapFrom(s => s.StartDateTime.ToString(Constants.DefaultDateFormat)))
                .ForMember(d => d.EndDateTime,
                    opt => opt.MapFrom(s => s.EndDateTime.ToString(Constants.DefaultDateFormat)))
                .ForMember(d => d.MovieName,
                    opt => opt.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.TheaterName,
                    opt => opt.MapFrom(s => s.Theater.Name)).ReverseMap();
        }
    }
}

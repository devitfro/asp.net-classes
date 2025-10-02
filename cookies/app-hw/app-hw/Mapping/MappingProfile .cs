using app_hw.Models;
using app_hw.ViewModels;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace app_hw.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsViewModel>()
                .ForMember(dest => dest.ShortContent,
                    opt => opt.MapFrom(src => src.Content.Length > 100 ? src.Content.Substring(0, 100) + "..." : src.Content))
                .ForMember(dest => dest.DateFormatted,
                    opt => opt.MapFrom(src => src.Date.ToString("dd.MM.yyyy")));
        }
    }
}

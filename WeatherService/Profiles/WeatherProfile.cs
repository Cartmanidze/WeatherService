using System;
using AutoMapper;
using WeatherMap.Client.Entities;
using WeatherService.Models;

namespace WeatherService.Profiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<Temperature, WeatherTemperatureResponse>()
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Value))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<Wind, WeatherWindResponse>()
                .ForMember(dest => dest.Speed, opt => opt.MapFrom(src => src.Speed))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => GetDirectionByDegrees(src.Degrees)))
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<TemperatureWithDate, WeatherTemperatureWithDateResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature.Value))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private string GetDirectionByDegrees(double degrees)
        {
            var directions = new[]
                {"north", "northeast", "east", "southeast", "south", "southwest", "west", "northwest"};
            degrees = degrees * 8 / 360;
            degrees = Math.Round(degrees, MidpointRounding.ToZero);
            degrees = (degrees + 8) % 8;
            return directions[(int)degrees];
        }
    }
}

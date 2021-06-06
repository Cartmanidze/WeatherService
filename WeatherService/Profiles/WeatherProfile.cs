using System;
using AutoMapper;
using WeatherMap.Client.Entities;
using WeatherService.Extensions;
using WeatherService.Helpers;
using WeatherService.Models;

namespace WeatherService.Profiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<Temperature, WeatherTemperatureResponse>()
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Value))
                .AfterMap((_, dest, context) =>
                {
                    dest.CityName = context.GetCityName();
                    dest.Metric = context.GetMetric();
                })
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<Wind, WeatherWindResponse>()
                .ForMember(dest => dest.Speed, opt => opt.MapFrom(src => src.Speed))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => WindHelper.GetDirectionByDegrees(src.Degrees)))
                .AfterMap((_, dest, context) =>
                {
                    dest.CityName = context.GetCityName();
                })
                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<TemperatureWithDate, WeatherTemperatureWithDateResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature.Value))
                .AfterMap((_, dest, context) =>
                {
                    dest.CityName = context.GetCityName();
                    dest.Metric = context.GetMetric();
                })
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}

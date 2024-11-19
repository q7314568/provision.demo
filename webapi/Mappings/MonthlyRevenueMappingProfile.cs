using AutoMapper;
using webapi.Models.Dto;
using webapi.Models;

namespace webapi.Mappings
{
    public class MonthlyRevenueMappingProfile : Profile
    {
        public MonthlyRevenueMappingProfile()
        {
            CreateMap<MonthlyRevenueSummary, MonthlyRevenueSummaryDto>().ReverseMap();
        }
    }
}

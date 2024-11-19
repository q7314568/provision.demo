using MediatR;
using webapi.Models.Dto;

namespace webapi.Queries
{
    public class GetMonthlyRevenueSummaryQuery : IRequest<PagedResult<MonthlyRevenueSummaryDto>>
    {
        public string? CompanyCode
        {
            get; set;
        }
        public string? DataYearMonth
        {
            get; set;
        }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

using MediatR;
using webapi.Models.Dto;

namespace webapi.Commands
{
    public class AddMonthlyRevenueCommand : IRequest<MonthlyRevenueSummaryDto>
    {
        public MonthlyRevenueSummaryDto Dto
        {
            get; set;
        }

        public AddMonthlyRevenueCommand(MonthlyRevenueSummaryDto dto)
        {
            Dto = dto;
        }
    }
}

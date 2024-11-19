using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using webapi.Commands;
using webapi.Handlers;
using webapi.Models.Dto;
using webapi.Queries;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyRevenueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MonthlyRevenueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("query")]
        public async Task<ActionResult<PagedResult<MonthlyRevenueSummaryDto>>> Get([FromBody] GetMonthlyRevenueSummaryQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<MonthlyRevenueSummaryDto>> Add([FromBody] MonthlyRevenueSummaryDto dto, CancellationToken cancellationToken)
        {
            var command = new AddMonthlyRevenueCommand(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new
            {
                companyCode = dto.CompanyCode,
                dataYearMonth = dto.DataYearMonth
            }, result);
        }
    }
}

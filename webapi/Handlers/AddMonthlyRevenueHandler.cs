using AutoMapper;
using MediatR;
using webapi.Models.Dto;
using webapi.Models;
using webapi.Commands;

namespace webapi.Handlers
{
    public class AddMonthlyRevenueHandler : IRequestHandler<AddMonthlyRevenueCommand, MonthlyRevenueSummaryDto>
    {
        private readonly MyLocalDBContext _context;
        private readonly IMapper _mapper;

        public AddMonthlyRevenueHandler(MyLocalDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MonthlyRevenueSummaryDto> Handle(AddMonthlyRevenueCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<MonthlyRevenueSummary>(request.Dto);
            await _context.MonthlyRevenueSummary.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<MonthlyRevenueSummaryDto>(entity);
        }
    }
}

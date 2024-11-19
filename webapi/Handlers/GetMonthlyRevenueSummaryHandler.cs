using MediatR;
using webapi.Models.Dto;
using webapi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using webapi.Queries;

namespace webapi.Handlers
{
    public class GetMonthlyRevenueSummaryHandler : IRequestHandler<GetMonthlyRevenueSummaryQuery, PagedResult<MonthlyRevenueSummaryDto>>
    {
        private readonly MyLocalDBContext _context;
        private readonly IMapper _mapper;

        public GetMonthlyRevenueSummaryHandler(MyLocalDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<MonthlyRevenueSummaryDto>> Handle(GetMonthlyRevenueSummaryQuery request, CancellationToken cancellationToken)
        {
            var query = _context.MonthlyRevenueSummary.AsQueryable();

            if (!string.IsNullOrEmpty(request.CompanyCode))
            {
                query = query.Where(x => x.CompanyCode == request.CompanyCode);
            }

            if (!string.IsNullOrEmpty(request.DataYearMonth))
            {
                query = query.Where(x => x.DataYearMonth == request.DataYearMonth);
            }

            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            var totalRecords = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var mappedItems = items.Select(x => _mapper.Map<MonthlyRevenueSummaryDto>(x)).ToList();

            return new PagedResult<MonthlyRevenueSummaryDto>
            {
                Items = mappedItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<MonthlyRevenueSummaryDto> AddMonthlyRevenueAsync(MonthlyRevenueSummaryDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<MonthlyRevenueSummary>(dto);
            await _context.MonthlyRevenueSummary.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<MonthlyRevenueSummaryDto>(entity);
        }
    }
}

using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;
using webapi.Models;
using webapi.Models.Dto;

namespace webapi.helper
{
    public class CsvImporter
    {
        private readonly MyLocalDBContext _context;

        public CsvImporter(MyLocalDBContext context)
        {
            _context = context;
        }

        public async Task ImportCsvDataAsync(string csvFilePath)
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [MonthlyRevenueSummary]");
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<MonthlyRevenueSummaryDto>();
                foreach (var record in records)
                {
                    await _context.Procedures.AddMonthlyRevenueSummaryAsync(
                        record.ReportDate,
                        record.DataYearMonth,
                        record.CompanyCode,
                        record.CompanyName,
                        record.Industry,
                        record.CurrentRevenue,
                        record.LastMonthRevenue,
                        record.LastYearSameMonthRevenue,
                        record.MonthlyChangePercent,
                        record.YearlyChangePercent,
                        record.CumulativeCurrentRevenue,
                        record.CumulativeLastYearRevenue,
                        record.CumulativeChangePercent,
                        record.Remarks
                    );
                }
            }
        }
    }
}

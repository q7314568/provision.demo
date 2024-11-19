namespace webapi.Models.Dto
{
    public class MonthlyRevenueSummaryDto
    {
        public string ReportDate
        {
            get; set;
        }
        public string DataYearMonth
        {
            get; set;
        }
        public string CompanyCode
        {
            get; set;
        }
        public string CompanyName
        {
            get; set;
        }
        public string Industry
        {
            get; set;
        }
        public long CurrentRevenue
        {
            get; set;
        }
        public long LastMonthRevenue
        {
            get; set;
        }
        public long LastYearSameMonthRevenue
        {
            get; set;
        }
        public double? MonthlyChangePercent
        {
            get; set;
        }
        public double? YearlyChangePercent
        {
            get; set;
        }
        public long CumulativeCurrentRevenue
        {
            get; set;
        }
        public long CumulativeLastYearRevenue
        {
            get; set;
        }
        public double? CumulativeChangePercent
        {
            get; set;
        }
        public string Remarks
        {
            get; set;
        }
    }
}

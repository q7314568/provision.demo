-- 1. 建立資料表
CREATE TABLE [dbo].[MonthlyRevenueSummary](
	[ReportDate] [nvarchar](7) NOT NULL,
	[DataYearMonth] [nvarchar](6) NOT NULL,
	[CompanyCode] [nvarchar](10) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[Industry] [nvarchar](50) NOT NULL,
	[CurrentRevenue] [bigint] NOT NULL,
	[LastMonthRevenue] [bigint] NOT NULL,
	[LastYearSameMonthRevenue] [bigint] NOT NULL,
	[MonthlyChangePercent] [float] NULL,
	[YearlyChangePercent] [float] NULL,
	[CumulativeCurrentRevenue] [bigint] NOT NULL,
	[CumulativeLastYearRevenue] [bigint] NOT NULL,
	[CumulativeChangePercent] [float] NULL,
	[Remarks] [nvarchar](max) NULL,
 CONSTRAINT [PK_MonthlyRevenueSummary] PRIMARY KEY CLUSTERED 
(
	[DataYearMonth] ASC,
	[CompanyCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE PROCEDURE [dbo].[AddMonthlyRevenueSummary]
    @ReportDate NVARCHAR(7),
    @DataYearMonth NVARCHAR(6),
    @CompanyCode NVARCHAR(10),
    @CompanyName NVARCHAR(50),
    @Industry NVARCHAR(50),
    @CurrentRevenue BIGINT,
    @LastMonthRevenue BIGINT,
    @LastYearSameMonthRevenue BIGINT,
    @MonthlyChangePercent FLOAT,
    @YearlyChangePercent FLOAT,
    @CumulativeCurrentRevenue BIGINT,
    @CumulativeLastYearRevenue BIGINT,
    @CumulativeChangePercent FLOAT,
    @Remarks NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO MonthlyRevenueSummary (
        ReportDate, DataYearMonth, CompanyCode, CompanyName, Industry,
        CurrentRevenue, LastMonthRevenue, LastYearSameMonthRevenue,
        MonthlyChangePercent, YearlyChangePercent,
        CumulativeCurrentRevenue, CumulativeLastYearRevenue,
        CumulativeChangePercent, Remarks
    )
    VALUES (
        @ReportDate, @DataYearMonth, @CompanyCode, @CompanyName, @Industry,
        @CurrentRevenue, @LastMonthRevenue, @LastYearSameMonthRevenue,
        @MonthlyChangePercent, @YearlyChangePercent,
        @CumulativeCurrentRevenue, @CumulativeLastYearRevenue,
        @CumulativeChangePercent, @Remarks
    );
END;
GO

-- 3. 儲存程序 - 查詢資料
CREATE PROCEDURE GetMonthlyRevenueSummary
    @CompanyCode NVARCHAR(10) = NULL,
    @DataYearMonth NVARCHAR(6) = NULL
AS
BEGIN
    SELECT 
        Id, ReportDate, DataYearMonth, CompanyCode, CompanyName, Industry,
        CurrentRevenue, LastMonthRevenue, LastYearSameMonthRevenue,
        MonthlyChangePercent, YearlyChangePercent,
        CumulativeCurrentRevenue, CumulativeLastYearRevenue,
        CumulativeChangePercent, Remarks
    FROM 
        MonthlyRevenueSummary
    WHERE
        (@CompanyCode IS NULL OR CompanyCode = @CompanyCode) AND
        (@DataYearMonth IS NULL OR DataYearMonth = @DataYearMonth);
END;

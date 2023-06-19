USE [smallbiz]
GO

/****** Object:  Table [dbo].[SmallBusiness]    Script Date: 6/19/2023 11:56:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SmallBusiness](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Sales] [float] NULL,
	[OwnerSalary] [float] NULL,
	[Depreciation] [float] NULL,
	[Interest] [float] NULL,
	[OwnerPersonalExpenses] [float] NULL,
	[Utilities] [float] NULL,
	[Rent] [float] NULL,
	[Payroll] [float] NULL,
	[MiscExpenses] [float] NULL,
	[SDEMultiple] [float] NULL,
	[SellableInventory] [float] NULL,
	[AskingPrice] [float] NULL,
	[Sde]  AS ([AskingPrice]*[SellableInventory]),
	[HealthRatio]  AS ([AskingPrice]*[SellableInventory]),
	[SDEValuation]  AS ([SDEMultiple]*[MiscExpenses]),
	[PriceDelta]  AS ([Rent]*[Utilities]),
 CONSTRAINT [PK_SmallBusiness] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
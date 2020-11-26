USE [MyDatabase]
GO

/****** Object:  Table [dbo].[employee_details]    Script Date: 11/26/2020 10:11:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[employee_details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fname] [varchar](45) NULL,
	[lname] [varchar](45) NULL,
	[gender] [varchar](45) NULL,
	[address] [varchar](45) NULL,
	[phone] [varchar](45) NULL,
	[email] [varchar](45) NULL,
	[img] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



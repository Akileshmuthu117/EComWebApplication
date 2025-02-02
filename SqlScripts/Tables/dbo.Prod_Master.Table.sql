/****** Object:  Table [dbo].[Prod_Master]    Script Date: 20-05-2024 16:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prod_Master](
	[ProdID] [int] IDENTITY(1,1) NOT NULL,
	[CatID] [int] NOT NULL,
	[ProdName] [varchar](100) NOT NULL,
	[ProdImg] [varchar](500) NULL,
	[Price] [money] NOT NULL,
	[ProdDesc] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Prod_Master]  WITH CHECK ADD FOREIGN KEY([CatID])
REFERENCES [dbo].[Cat_Master] ([CatID])
GO

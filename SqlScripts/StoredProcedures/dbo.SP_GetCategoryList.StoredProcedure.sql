/****** Object:  StoredProcedure [dbo].[SP_GetCategoryList]    Script Date: 20-05-2024 16:53:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_GetCategoryList] as
begin

 select 
	CatID,
	CatName,
	CatImage,
	CatDesc
 from Cat_Master with (nolock)

end
GO

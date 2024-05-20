/****** Object:  StoredProcedure [dbo].[SP_GetProductList]    Script Date: 20-05-2024 16:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_GetProductList](@CatID int) as
begin

 select ProdID, ProdName, ProdImg, Price, ProdDesc from Prod_Master with (nolock) where CatID = @CatID

end
GO

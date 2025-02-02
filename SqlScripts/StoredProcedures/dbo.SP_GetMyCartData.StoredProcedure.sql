/****** Object:  StoredProcedure [dbo].[SP_GetMyCartData]    Script Date: 20-05-2024 16:53:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_GetMyCartData] as
begin


	select prm.ProdID, prm.ProdName, myc.Qty, prm.Price,  myc.Qty * prm.Price TotalPrice  
	from	   MyCart      myc with (nolock)
	inner join Prod_Master prm with (nolock) on myc.ProdID = prm.ProdID

end
GO

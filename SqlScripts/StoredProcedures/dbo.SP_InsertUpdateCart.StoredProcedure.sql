/****** Object:  StoredProcedure [dbo].[SP_InsertUpdateCart]    Script Date: 20-05-2024 16:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_InsertUpdateCart](@ProdID int, @Qty int) as
begin

 if exists(select 1 from MyCart with (nolock) where ProdID = @ProdID)
 begin
	update MyCart set qty = @Qty where ProdID = @ProdID
 end
 else
 begin
	insert into MyCart (ProdID, Qty)
	select @ProdID, @Qty
 end

end
GO

USE pricecalculator;

create procedure [dbo].[SaveCart]  
    @sessionId     varchar(255),  
    @item       varchar(50),  
    @quantity   varchar(50) 
AS  
BEGIN  
DECLARE @ItemId int;
select @ItemId = Id from items where Name = @item;
IF EXISTS (select 1 from Cart where sessionid = @sessionId and itemid = @ItemId)
BEGIN
    Update cart set quantity = @quantity
    where sessionid = @sessionId and itemid = @ItemId;
END
ELSE
BEGIN
    Insert into Cart Values(@sessionId,@ItemId,@quantity);
END
END;
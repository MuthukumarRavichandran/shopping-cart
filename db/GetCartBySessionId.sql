USE pricecalculator;

CREATE PROCEDURE [dbo].[GetCartBySessionId]
    @sessionId varchar(255)
AS
BEGIN
    select its.Name as ItemName, crt.quantity from Cart crt
    inner join items its on its.Id = crt.itemid
    WHERE crt.sessionid = @sessionId
END;
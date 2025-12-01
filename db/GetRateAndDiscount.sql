USE pricecalculator;

create PROCEDURE [dbo].[GetRateAndDiscount]
AS
BEGIN
    select its.Name as Item, itp.price as Rate, itd.discount from items its
    inner join itemprice itp
    on its.Id = itp.itemid
    inner join itemdiscount itd
    on its.Id = itd.itemid;
END;
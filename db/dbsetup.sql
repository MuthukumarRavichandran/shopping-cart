CREATE DATABASE pricecalculator1;

USE pricecalculator;

CREATE TABLE items
(
Id int  identity(1,1),
Name varchar(255) not null unique,
CONSTRAINT PK_items PRIMARY KEY (Id)
);

CREATE TABLE itemprice
(
Id int  identity(1,1),
itemid int NOT NULL unique,
price int not null,
CONSTRAINT PK_itemprice PRIMARY KEY (Id),
CONSTRAINT fk_itemprice FOREIGN KEY (itemid) REFERENCES items(Id)
);

CREATE TABLE itemdiscount
(
Id int  identity(1,1),
itemid int NOT NULL unique,
discount int not null,
CONSTRAINT PK_itemdiscount PRIMARY KEY (Id),
CONSTRAINT fk_itemdiscount FOREIGN KEY (itemid) REFERENCES items(Id)
);

Create table Cart
(
Id int  identity(1,1),
sessionid varchar(255) not null,
itemid int NOT NULL,
quantity int not null,
CONSTRAINT PK_cart PRIMARY KEY (Id),
CONSTRAINT fk_chart_item FOREIGN KEY (itemid) REFERENCES items(Id),
CONSTRAINT UQ_usesrid_itemid UNIQUE(userid, itemid)
);

insert into items values('apple');
insert into items values('orange');
insert into items values('banana');

insert into itemprice values (1,50);
insert into itemprice values (2,75);
insert into itemprice values (3,150);

insert into itemdiscount values (1,5);
insert into itemdiscount values (2,10);
insert into itemdiscount values (3,15);


CREATE PROCEDURE [dbo].[GetCartBySessionId]
    @sessionId varchar(255)
AS
BEGIN
    select its.Name as ItemName, crt.quantity from Cart crt
    inner join items its on its.Id = crt.itemid
    WHERE crt.sessionid = @sessionId
END;

create PROCEDURE [dbo].[GetRateAndDiscount]
AS
BEGIN
    select its.Name as Item, itp.price as Rate, itd.discount from items its
    inner join itemprice itp
    on its.Id = itp.itemid
    inner join itemdiscount itd
    on its.Id = itd.itemid;
END;

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
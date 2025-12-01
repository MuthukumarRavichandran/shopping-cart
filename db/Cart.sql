USE pricecalculator;

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
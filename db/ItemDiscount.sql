USE pricecalculator;

CREATE TABLE itemdiscount
(
Id int  identity(1,1),
itemid int NOT NULL unique,
discount int not null,
CONSTRAINT PK_itemdiscount PRIMARY KEY (Id),
CONSTRAINT fk_itemdiscount FOREIGN KEY (itemid) REFERENCES items(Id)
);
USE pricecalculator;

CREATE TABLE itemprice
(
Id int  identity(1,1),
itemid int NOT NULL unique,
price int not null,
CONSTRAINT PK_itemprice PRIMARY KEY (Id),
CONSTRAINT fk_itemprice FOREIGN KEY (itemid) REFERENCES items(Id)
);
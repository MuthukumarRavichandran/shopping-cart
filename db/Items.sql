USE pricecalculator;

CREATE TABLE items
(
Id int  identity(1,1),
Name varchar(255) not null unique,
CONSTRAINT PK_items PRIMARY KEY (Id)
);
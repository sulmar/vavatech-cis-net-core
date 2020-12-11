
create database CISDb

go

use CISDb

go

create table dbo.Customers
(
	CustomerId int identity not null primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Gender nvarchar(10) not null,
	Email varchar(250) null,
	Username nvarchar(50) not null,
	HashedPassword varchar(50) not null,
	Pesel char(11) not null,
	Salary numeric(18, 2) null,
	IsRemoved bit not null
)

go
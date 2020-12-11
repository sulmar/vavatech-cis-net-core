
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



create table dbo.Reports (
		ReportId int identity not null primary key,
		Title nvarchar(100) not null,
		CreateDate datetime2 not null,
		PeriodFrom datetime2 not null,
		PeriodTo datetime2 not null,
		CustomerId int not null -- FK 
)


insert into dbo.Reports values('Raport 1', GETDATE(), GETdate(), Getdate(), 1)
insert into dbo.Reports values('Raport 2', GETDATE(), GETdate(), Getdate(), 1)
insert into dbo.Reports values('Raport 3', GETDATE(), GETdate(), Getdate(), 2)



select ReportId, Title, CreateDate, PeriodFrom, PeriodTo, c.*
from dbo.Reports as r inner join dbo.Customers as c
	on r.CustomerId = c.CustomerId




 

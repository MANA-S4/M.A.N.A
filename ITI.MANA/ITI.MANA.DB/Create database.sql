create database [ITI.S4.MANA];

use [ITI.S4.MANA];
GO

if exists(SELECT * FROM sys.objects WHERE name='PK_Tasks')
begin 
	ALTER TABLE iti.tasks   
DROP CONSTRAINT PK_Tasks
end
GO

if exists(SELECT * FROM sys.objects WHERE name='FK_Tasks_UserId')
begin 
	ALTER TABLE iti.tasks   
DROP CONSTRAINT FK_Tasks_UserId
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'PK_Relationships')
begin 
	ALTER TABLE iti.Relationships   
DROP CONSTRAINT PK_Relationships
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'PK_UserId')
begin 
	ALTER TABLE iti.Users   
DROP CONSTRAINT PK_UserId
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Tasks' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Tasks;
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Relationships' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Relationships;
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Events' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Events;
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Notifications' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Notifications;
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Users' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Users;
end
GO

if exists(select * from sys.schemas s where s.[name] = 'iti')
begin
	exec('drop schema iti;');
end
GO

create schema iti;
GO

create table iti.Users
(
	UserId  int identity(1, 1),
	FirstName nvarchar(32),
	LastName  nvarchar(32),
	BirthDate datetime2,
	UserPassword binary(32),

	constraint PK_UserId primary key(UserId)
);

create table iti.Tasks
(
	TaskID  int identity(1, 1),
	TaskName nvarchar(32),
	TaskDate datetime2,
	IsFinish bit,
	IsPrivate bit,
	UserId int,

	constraint PK_Tasks primary key(TaskID),
	constraint FK_Tasks_UserId foreign key(UserId) references iti.Users(UserId)
);

create table iti.Relationships
(
	RelationID  int identity(1, 1),
	UserId int,
	RelationType  nvarchar(32),
	UserRelationID int,
	
	constraint PK_Tasks primary key(RelationID),
	constraint FK_Tasks_UserId foreign key(UserId) references iti.Users(UserId)
);

create table iti.Events
(
	EventId  int identity(1, 1),
	EventName nvarchar(32),
	EventDate  datetime2,
	IsFinish bit,
	IsPrivate bit,
	Members varchar(32),

	constraint PK_Events primary key(EventId)
);

create table iti.Notifications
(
	NotificationId  int identity(1, 1),
	NotificationDate datetime2,
	Message  nvarchar(32),
	WasRead bit,
	UserId int,

	constraint PK_Notifications primary key(UserId),
	constraint FK_Notifications_UserId foreign key(UserId) references iti.Users
);
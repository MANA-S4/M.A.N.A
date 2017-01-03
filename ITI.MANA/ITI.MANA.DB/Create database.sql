create database [ITI.S4.MANA];
GO

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

if exists(SELECT * FROM sys.objects WHERE name='FK_Notifications_UserId')
begin 
	ALTER TABLE iti.notifications 
DROP CONSTRAINT FK_Notifications_UserId
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'PK_Relationships')
begin 
	ALTER TABLE iti.Relationships   
DROP CONSTRAINT PK_Relationships
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'FK_Relationships_UserId')
begin 
	ALTER TABLE iti.Relationships   
DROP CONSTRAINT FK_Relationships_UserId
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'PK_UserId')
begin 
	ALTER TABLE iti.Users   
DROP CONSTRAINT PK_UserId
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'PK_GoogleUserId')
begin 
	ALTER TABLE iti.GoogleUser   
DROP CONSTRAINT PK_GoogleUserId
end
GO

if exists(SELECT * FROM sys.objects WHERE name = 'PK_MicrosoftUserId')
begin 
	ALTER TABLE iti.MicrosoftUser   
DROP CONSTRAINT PK_MicrosoftUserId
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Tasks' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Tasks;
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'Contacts' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.Contacts;
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

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'GoogleUser' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.GoogleUser;
end
GO

if exists(select * from INFORMATION_SCHEMA.TABLES t where t.TABLE_NAME = 'MicrosoftUser' and t.TABLE_SCHEMA = 'iti')
begin
	drop table iti.MicrosoftUser;
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
	Email nvarchar(64),
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

create table iti.Contacts
(
	ContactId  int identity(1, 1),
	UserId int,
	RelationType  nvarchar(32),
	UserRelationId int,
	
	constraint PK_Contacts primary key(ContactId),
	constraint FK_Contacts_UserId foreign key(UserId) references iti.Users(UserId)
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

create table iti.GoogleUser
(
	UserId int identity(1,1),
	AccessToken varchar(64) not null,
	RefreshToken varchar(64),
	TokenType varchar(64),
	ExpireIn datetime2
	
	constraint PK_GoogleUserId primary key(UserId),
	constraint FK_GoogleUser_UserId foreign key(UserId) references iti.Users(UserId)
);

create table iti.MicrosoftUser
(
	UserId int identity(1,1),
	AccessToken varchar(64) not null,

	constraint PK_MicrosoftUserId primary key(UserId),
	constraint FK_MicrosoftUser_UserId foreign key(UserId) references iti.Users(UserId)
);

create table iti.PasswordUser
(
	UserId int identity(1,1),
	[Password] varbinary(128) not null,

	constraint PK_PasswordUserId primary key(UserId),
	constraint FK_PasswordUser_UserId foreign key(UserId) references iti.Users(UserId)
);
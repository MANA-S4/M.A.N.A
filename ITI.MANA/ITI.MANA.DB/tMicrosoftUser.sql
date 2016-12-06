create table iti.MicrosoftUser
(
	UserId int identity(1,1),
	AccessToken varchar(64) not null,

	constraint PK_MicrosoftUserId primary key(UserId),
	constraint FK_MicrosoftUser_UserId foreign key(UserId) references iti.Users(UserId)
);
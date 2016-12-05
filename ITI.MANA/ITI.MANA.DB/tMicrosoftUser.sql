create table iti.MicrosoftUser
(
	MicrosoftUserId int identity(1,1),
	AccessToken varchar(64) not null,

	constraint PK_MicrosoftUserId primary key(MicrosoftUserId),
	constraint FK_MicrosoftUser_UserId foreign key(MicrosoftUserId) references iti.Users(UserId)
);
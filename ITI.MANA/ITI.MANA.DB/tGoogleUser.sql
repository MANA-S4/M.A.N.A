create table iti.GoogleUser
(
	UserId int identity(1,1),
	RefreshToken varchar(64) not null,

	constraint PK_GoogleUserId primary key(UserId),
	constraint FK_GoogleUser_UserId foreign key(UserId) references iti.Users(UserId)
);
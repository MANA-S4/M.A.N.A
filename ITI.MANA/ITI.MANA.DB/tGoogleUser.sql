create table iti.GoogleUser
(
	GoogleUserId int identity(1,1),
	RefreshToken varchar(64) not null,

	constraint PK_GoogleUserId primary key(GoogleUserId),
	constraint FK_GoogleUser_UserId foreign key(GoogleUserId) references iti.Users(UserId)
);
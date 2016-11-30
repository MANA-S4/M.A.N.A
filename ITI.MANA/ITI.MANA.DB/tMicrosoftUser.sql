create table iti.tMicrosoftUser
(
	UserId       int,
	RefreshToken varchar(64) not null,

	constraint PK_tMicrosoftUser primary key(UserId),
	constraint FK_tMicrosoftUser_UserId foreign key(UserId) references iti.tUser(UserId)
);
create table iti.PasswordUser
(
	UserId     int,
	[Password] varbinary(128) not null,

	constraint PK_PasswordUser primary key(UserId),
	constraint FK_PasswordUser_UserId foreign key(UserId) references iti.Users(UserId)
);

drop table iti.PasswordUser;
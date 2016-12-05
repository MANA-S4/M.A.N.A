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
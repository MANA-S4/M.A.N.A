create procedure iti.sCompleteUserUpdate
(
	@UserId int,
	@Email  nvarchar(64),
	@LastName nvarchar(64),
	@FirstName nvarchar(64), 
	@BirthDate datetime2,
	@Password binary
)
as
begin
	update iti.Users
	set Email = @Email, 
	LastName = @LastName, 
	FirstName = @FirstName, 
	BirthDate = @BirthDate, 
	UserPassword = @Password
	where UserId = @UserId;
	return 0;
end;
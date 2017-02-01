create procedure iti.sUserInfoUpdate
(
	@UserId int,
	@Email  nvarchar(64),
	@LastName nvarchar(64),
	@FirstName nvarchar(64), 
	@BirthDate datetime2
)
as
begin
	update iti.Users
	set Email = @Email, 
	LastName = @LastName, 
	FirstName = @FirstName, 
	BirthDate = @BirthDate
	where UserId = @UserId;
	return 0;
end;
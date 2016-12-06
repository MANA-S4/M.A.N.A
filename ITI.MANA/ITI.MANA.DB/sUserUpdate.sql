create procedure iti.sUserUpdate
(
	@UserId int,
	@Email  nvarchar(64)
)
as
begin
	update iti.Users
	set Email = @Email
	where UserId = @UserId;
	return 0;
end;
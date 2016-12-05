create procedure iti.sPasswordUserUpdate
(
	@UserId   int,
	@Password varbinary(128)
)
as
begin
	update iti.PasswordUser set [Password] = @Password where UserId = @UserId;
	return 0;
end;
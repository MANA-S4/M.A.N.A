create procedure iti.sPasswordUserCreate
(
	@Email    nvarchar(64),
	@Password varbinary(128)
)
as
begin
	insert into iti.Users(Email) values(@Email);
	declare @userId int;
	select @userId = scope_identity();
	insert into iti.PasswordUser(UserId,  [Password])
	                       values(@userId, @Password);
	return 0;
end;
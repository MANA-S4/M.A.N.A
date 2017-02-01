create procedure iti.sUserDelete
(
	@UserId int
)
as
begin
	delete from iti.PasswordUser where UserId = @UserId;
	delete from iti.GoogleUser where UserId = @UserId;
	delete from iti.MicrosoftUser where UserId = @UserId;
	delete from iti.Users where UserId = @UserId;
	return 0;
end;
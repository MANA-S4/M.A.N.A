create procedure iti.sGoogleUserUpdate
(
	@UserId       int,
	@RefreshToken varchar(64)
)
as
begin
	update iti.GoogleUser set RefreshToken = @RefreshToken where UserId = @UserId;
	return 0;
end;
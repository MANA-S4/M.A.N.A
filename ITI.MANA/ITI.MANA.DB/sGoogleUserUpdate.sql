create procedure iti.sGoogleUserUpdate
(
	@GoogleUserId       int,
	@RefreshToken varchar(64)
)
as
begin
	update iti.GoogleUser set RefreshToken = @RefreshToken where GoogleUserId = @GoogleUserId;
	return 0;
end;
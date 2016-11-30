create procedure iti.sMicrosoftUserUpdate
(
	@UserId       int,
	@RefreshToken varchar(64)
)
as
begin
	update iti.tMicrosoftUser set RefreshToken = @RefreshToken where UserId = @UserId;
	return 0;
end;
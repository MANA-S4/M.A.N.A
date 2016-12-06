create procedure iti.sMicrosoftUserUpdate
(
	@UserId       int,
	@AccessToken varchar(64)
)
as
begin
	update iti.MicrosoftUser set AccessToken = @AccessToken where UserId = @UserId;
	return 0;
end;
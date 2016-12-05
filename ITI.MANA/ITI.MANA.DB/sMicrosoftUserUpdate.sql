create procedure iti.sMicrosoftUserUpdate
(
	@MicrosoftUserId       int,
	@AccessToken varchar(64)
)
as
begin
	update iti.MicrosoftUser set AccessToken = @AccessToken where MicrosoftUserId = @MicrosoftUserId;
	return 0;
end;
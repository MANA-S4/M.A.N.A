create procedure iti.sUserAddMicrosoftToken
(
	@MicrosoftUserId       int,
	@AccessToken  varchar(64)
)
as
begin
	insert into iti.MicrosoftUser(MicrosoftUserId,  AccessToken)
	                     values(@MicrosoftUserId, @AccessToken);
	return 0;
end;
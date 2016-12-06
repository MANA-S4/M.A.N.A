create procedure iti.sUserAddMicrosoftToken
(
	@UserId      int,
	@AccessToken  varchar(64)
)
as
begin
	insert into iti.MicrosoftUser(UserId,  AccessToken)
	                     values(@UserId, @AccessToken);
	return 0;
end;
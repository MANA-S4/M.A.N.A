create procedure iti.sUserAddGoogleToken
(
	@UserId int,
	@AccessToken varchar(64)
)
as
begin
	insert into iti.GoogleUser(UserId,  AccessToken)
	                     values(@UserId, @AccessToken);
	return 0;
end;
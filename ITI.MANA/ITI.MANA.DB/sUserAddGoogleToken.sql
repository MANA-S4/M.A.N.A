create procedure iti.sUserAddGoogleToken
(
	@GoogleUserId int,
	@RefreshToken varchar(64)
)
as
begin
	insert into iti.GoogleUser(GoogleUserId,  RefreshToken)
	                     values(@GoogleUserId, @RefreshToken);
	return 0;
end;
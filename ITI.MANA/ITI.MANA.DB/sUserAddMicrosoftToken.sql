create procedure iti.sUserAddMicrosoftToken
(
	@UserId       int,
	@RefreshToken varchar(64)
)
as
begin
	insert into iti.tMicrosoftUser(UserId,  RefreshToken)
	                     values(@UserId, @RefreshToken);
	return 0;
end;
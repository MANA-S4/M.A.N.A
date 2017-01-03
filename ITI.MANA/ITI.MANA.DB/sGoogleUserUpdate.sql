create procedure iti.sGoogleUserUpdate
(
	@UserId       int,
	@AccessToken  varchar(64),
	@RefreshToken varchar(64),
	@TokenType    varchar(64),
	@ExpireIn     datetime2
)
as
begin
	update iti.GoogleUser set AccessToken = @AccessToken,RefreshToken = @RefreshToken,TokenType = @TokenType,ExpireIn=@ExpireIn   where UserId = @UserId;
	return 0;
end;

drop proc iti.sGoogleUserUpdate

select * from iti.Users;
select * from iti.GoogleUser;
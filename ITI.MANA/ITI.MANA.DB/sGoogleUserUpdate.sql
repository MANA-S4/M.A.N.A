create procedure iti.sGoogleUserUpdate
(
	@UserId       int,
	@AccessToken  varchar(72),
	@RefreshToken varchar(72),
	@TokenType    varchar(64),
	@ExpireIn     bigint
)
as
begin
	update iti.GoogleUser set AccessToken = @AccessToken,TokenType = @TokenType,ExpireIn=@ExpireIn   where UserId = @UserId;
	return 0;
end;

drop proc iti.sGoogleUserUpdate;
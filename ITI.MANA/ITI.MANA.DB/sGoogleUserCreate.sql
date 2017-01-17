create procedure iti.sGoogleUserCreate
(
	@Email        nvarchar(64),
	@AccessToken  varchar(72),
	@RefreshToken varchar(72),
	@TokenType    varchar(64),
	@ExpireIn     bigint
)
as
begin
set identity_insert iti.GoogleUser on;
	insert into iti.Users(Email) values(@Email);
	declare @userId int;
	select @userId = scope_identity();
	insert into iti.GoogleUser(UserId,AccessToken,RefreshToken,TokenType, ExpireIn)
	                     values(@userId,@AccessToken,@RefreshToken,@TokenType,@ExpireIn);
	return 0;
end;

drop procedure iti.sGoogleUserCreate
  

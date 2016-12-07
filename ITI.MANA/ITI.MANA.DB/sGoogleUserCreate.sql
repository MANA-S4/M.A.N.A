create procedure iti.sGoogleUserCreate
(
	@Email        nvarchar(64),
	@RefreshToken varchar(64)
)
as
begin
set identity_insert iti.GoogleUser on;
	insert into iti.Users(Email) values(@Email);
	declare @userId int;
	select @userId = scope_identity();
	insert into iti.GoogleUser(UserId,RefreshToken)
	                     values(@userId,@RefreshToken);
	return 0;
end;

drop procedure iti.sGoogleUserCreate

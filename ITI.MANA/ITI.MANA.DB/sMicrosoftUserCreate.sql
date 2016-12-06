create procedure iti.sMicrosoftUserCreate
(
	@Email        nvarchar(64),
	@AccessToken varchar(64)
)
as
begin
	insert into iti.Users(Email) values(@Email);
	declare @userId int;
	select @userId = scope_identity();
	insert into iti.MicrosoftUser(UserId,  AccessToken)
	                     values(@userId, @AccessToken);
	return 0;
end;

drop procedure iti.sMicrosoftUserCreate
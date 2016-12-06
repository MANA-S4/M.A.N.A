﻿create procedure iti.sUserAddGoogleToken
(
	@UserId int,
	@RefreshToken varchar(64)
)
as
begin
	insert into iti.GoogleUser(UserId,  RefreshToken)
	                     values(@UserId, @RefreshToken);
	return 0;
end;
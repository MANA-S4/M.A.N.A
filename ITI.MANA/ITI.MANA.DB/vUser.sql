﻿create view iti.vUser
as
	select UserId = u.UserId,
           Email = u.Email,
           [Password] = case when p.[Password] is null then 0x else p.[Password] end,
           MicrosoftAccessToken = case when mc.AccessToken is null then '' else mc.AccessToken end,
           GoogleRefreshToken = case when gl.RefreshToken is null then '' else gl.RefreshToken end
	from iti.tUser u
		left outer join iti.tPasswordUser p on p.UserId = u.UserId
		left outer join iti.tMicrosoftUser mc on mc.UserId = u.UserId
		left outer join iti.tGoogleUser gl on gl.UserId = u.UserId
	where u.UserId <> 0;
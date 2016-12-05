create view iti.vUser
as
	select UserId = u.UserId,
           Email = u.Email,
           UserPassword = case when u.UserPassword is null then 0x else u.UserPassword end,
           MicrosoftAccessToken = case when mc.AccessToken is null then '' else mc.AccessToken end,
           GoogleRefreshToken = case when gl.RefreshToken is null then '' else gl.RefreshToken end
	from iti.Users u
		left outer join iti.MicrosoftUser mc on mc.MicrosoftUserId = u.UserId
		left outer join iti.GoogleUser gl on gl.GoogleUserId = u.UserId
	where u.UserId <> 0;
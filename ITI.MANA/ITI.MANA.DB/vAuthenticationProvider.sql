create view iti.vAuthenticationProvider
as
	select usr.UserId, usr.ProviderName
	from (select UserId = u.UserId,
			  ProviderName = 'MANA'
		  from iti.PasswordUser u
		  union
		  select UserId = u.MicrosoftUserId,
			  ProviderName = 'Microsoft'
		  from iti.MicrosoftUser u
		  union
		  select UserId = u.GoogleUserId,
			  ProviderName = 'Google'
		  from iti.GoogleUser u) usr
	where usr.UserId <> 0;
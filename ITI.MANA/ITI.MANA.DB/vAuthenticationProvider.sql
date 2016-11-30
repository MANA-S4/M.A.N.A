create view iti.vAuthenticationProvider
as
	select usr.UserId, usr.ProviderName
	from (select UserId = u.UserId,
			  ProviderName = 'MANA'
		  from iti.tPasswordUser u
		  union
		  select UserId = u.UserId,
			  ProviderName = 'Microsoft'
		  from iti.tMicrosoftUser u
		  union
		  select UserId = u.UserId,
			  ProviderName = 'Google'
		  from iti.tGoogleUser u) usr
	where usr.UserId <> 0;
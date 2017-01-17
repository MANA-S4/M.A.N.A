create procedure iti.sContactUpdate
(
	@ContactId int,
	@RelationType  nvarchar(64)
)
as
begin
	update iti.Contacts
	set RelationType = @RelationType
	where ContactId = @ContactId;
	return 0;
end;

drop proc iti.sContactUpdate;

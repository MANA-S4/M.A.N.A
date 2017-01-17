create procedure iti.sContactDelete
(
	@ContactId int
)
as
begin
	delete from iti.Contacts where ContactId = @ContactId;
	return 0;
end;

drop proc iti.sContactDelete;

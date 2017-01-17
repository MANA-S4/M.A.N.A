create procedure iti.sDeleteEvent
(
	@EventId int
)
as
begin
	delete from iti.Events where EventId = @EventId;
	return 0;
end;
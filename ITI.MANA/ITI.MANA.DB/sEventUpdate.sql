create procedure iti.sEventUpdate
(
	@EventId	int,
	@EventName	nvarchar(max),
	@EventDate	datetime2,
	@Members	nvarchar(max),
	@IsPrivate	bit
)
as
begin
	update iti.Events set EventName = @EventName, EventDate = @EventDate, Members = @Members, IsPrivate = @IsPrivate where EventId = @EventId;
	return 0;
end;
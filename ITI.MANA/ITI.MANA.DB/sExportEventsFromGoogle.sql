create procedure iti.sExportEventsFromGoogle
(
	@UserId	int,
	@Json	nvarchar(max)
)
as
begin
	declare @temp table(EventId int,Etag nvarchar(max));
	declare @events table(Etag nvarchar(max),Summary nvarchar(max), [Date] datetime2);
	insert into @events(Etag,Summary, [Date])
	select * from OPENJSON(@json)
	with (
	Etag nvarchar(max) '$.Etag',
	Summary nvarchar(max) '$.Summary',
	[Date] Datetime2 '$.Date'
	);

	merge iti.[Events] as target
	using (select * from @events) as source (Etag,Summary,[Date])
	on (target.EventName = source.Summary)
	When not matched by target then
		Insert(EventName,UserId,EventDate,IsPrivate)
		Values(source.Summary,@UserId, source.[Date],1)
	Output inserted.EventId, source.Etag
	into @temp;

	merge iti.GoogleEvents as target
	using (select * from @temp) as source (EventId, Etag)
	on (target.EventId = source.EventId)
	When not matched by target then
		Insert(EventId,Etag)
		Values (source.EventId,source.Etag);
				
	return 0;
end;
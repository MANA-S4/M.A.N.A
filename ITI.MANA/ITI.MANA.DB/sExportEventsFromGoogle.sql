create procedure iti.sExportEventsFromGoogle
(
	@UserId	int,
	@Json	nvarchar(max)
)
as
begin
	declare @events table(Etag nvarchar(max),Summary nvarchar(max), [Date] datetime2);
	insert into @events(Etag,Summary, [Date])
	select * from OPENJSON(@json)
	with (
	Etag nvarchar(max) '$.Etag',
	Summary nvarchar(max) '$.Summary',
	[Date] Datetime2 '$.Date'
	);

	merge iti.[Events] as target
	using (select * from @events) as source (Etag, Summary, [Date])
	on (target.Etag = source.Etag)
	When not matched by target then
		Insert(Etag, EventName, EventDate)
	Values (source.Etag, source.Summary, Source.[Date]);

	return 0;
end;

drop procedure iti.sExportEventsFromGoogle


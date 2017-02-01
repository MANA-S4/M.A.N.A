create procedure iti.sEventCreate
(
	@EventName varchar(72),
	@EventDate datetime2,
	@UserId int,
	@IsPrivate  bit,
	@Members nvarchar(max)
)	
as
begin
	insert into iti.Events(EventName,EventDate,UserId,Members,IsPrivate) values(@EventName,@EventDate,@UserId,@Members,@IsPrivate);
	return 0;
end;
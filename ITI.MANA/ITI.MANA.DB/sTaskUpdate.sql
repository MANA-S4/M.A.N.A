create proc iti.sTaskUpdate
(
	@TaskId int,
	@TaskName nvarchar(64),
	@TaskDate datetime2
)

as 
begin

	update iti.Tasks
	set
		TaskName = @TaskName, TaskDate = @TaskDate
	where
		TaskId = @TaskId;

return 0;
end;

drop proc iti.sTaskUpdate;
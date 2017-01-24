alter proc iti.sTaskCreate 
(
	@TaskName nvarchar(64),
	@UserId int,
	@TaskDate datetime2,
	@AttributeTo int
)

as 
begin

insert into iti.Tasks(TaskName, UserId, TaskDate, AttributeTo)
	values(@TaskName, @UserId, @TaskDate,@AttributeTo);

return 0;
end;

drop proc iti.sTaskCreate;

select * from iti.Tasks
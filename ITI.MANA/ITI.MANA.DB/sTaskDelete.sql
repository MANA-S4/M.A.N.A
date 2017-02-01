create proc iti.sTaskDelete
(
	@TaskId int
)

as
begin

	delete from iti.Tasks 
	where
		TaskId = @TaskId;

	return 0;

end;
create proc iti.sTaskCreate 
(
	@TaskName nvarchar(64),
	@UserId int
)

as 
begin

insert into iti.Tasks(TaskName, UserId)
	values(@TaskName, @UserId);

return 0;
end;

drop proc iti.sTaskCreate;
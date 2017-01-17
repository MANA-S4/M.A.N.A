alter proc iti.sContactCreate
(
	@UserId int,
	@RelationType nvarchar(64),
	@UserRelationId int
)
as 
begin 
	declare @ContactId int;
	insert into iti.Contacts(UserId,RelationType,UserRelationId)
	                     values(@UserId, @RelationType, @UserRelationId);
	select @ContactId = scope_identity();
	return 0;
end;

drop procedure iti.sContactCreate;

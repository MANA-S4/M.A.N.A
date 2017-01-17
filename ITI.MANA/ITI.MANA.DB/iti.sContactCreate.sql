alter proc iti.sContactCreate
(
	@UserId int,
	@RelationType nvarchar(64),
	@ContactRelationType nvarchar(64),
	@UserRelationId int
)
as 
begin 
	insert into iti.Contacts(UserId,RelationType,UserRelationId)
		values(@UserId, @RelationType, @UserRelationId);
	insert into iti.Contacts(UserId,RelationType,UserRelationId)
		values(@UserRelationId,@ContactRelationType,@UserId);
	return 0;
end;

drop procedure iti.sContactCreate;

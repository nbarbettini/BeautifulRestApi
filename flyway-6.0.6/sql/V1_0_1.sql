alter table users alter column Guid UNIQUEIDENTIFIER null
go

alter table users add default NEWID() for Guid
go


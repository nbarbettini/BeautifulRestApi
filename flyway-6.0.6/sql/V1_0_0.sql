create table users
(
    guid uniqueidentifier default NEWID() not null,
    name text,
    email text
)
go

create unique index users_guid_uindex
    on users (guid)
go

alter table users
    add constraint users_pk
        primary key nonclustered (guid)
go


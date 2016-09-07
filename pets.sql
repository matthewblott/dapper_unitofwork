create table Breeds
(
  BreedId int identity(1,1) not null,
  Name nvarchar(200) not null,
  constraint pk_Breeds primary key clustered
  (
    BreedId asc
  )
  on [PRIMARY],
  constraint ux_Breeds unique nonclustered
  (
    Name asc
  )
  on [primary]
)
on [primary]
go

create table Cats
(
  CatId int identity(1,1) not null,
  BreedId int not null,
  Name nvarchar(200) not null,
  Age int not null,
  constraint pk_Cats primary key clustered
  (
    CatId asc
  )
  on [primary]
)
on [primary]
go

alter table Cats with check add constraint fk_Cats_Breed foreign key (BreedId)
references Breeds (BreedId)
go

alter table Cats check constraint fk_Cats_Breed
go

use FlightDbContext
go

insert into Roles values('Admin')
go
insert into Roles values('GO Employee')
go
insert into Roles values('Pilot')
go
insert into Roles values('Stewardess')
go

insert into Groups values('Pilot', '2021-01-10', 'Some note for pilot')
go
insert into Groups values('Stewardess', '2021-01-10', 'Some notification for crew')
go

insert into DocumentTypes values('Load Summary', '2021-01-10', 'Some note')
go
insert into DocumentTypes values('Update Summary', '2021-02-12', 'Some note')
go

insert into Flights values('VJ001', 'Ha Noi', 'Ho Chi Minh', '2022-03-12')
go
insert into Flights values('VJ002', 'Ho Chi Minh', 'Con Dao', '2022-07-01')
go 

insert into Documents values('Document1', '2022-03-11', 'Some note', '1.0', 1, 1, 1, 1)
go

insert into Documents values('Document2', '2022-03-12', 'Update note', '1.1', 1, 2, 2, 2)
go

insert into Documents values('Document3', '2022-06-30', 'Some note', '1.0', 2, 1, 2, 2)
go

use FlightDbContext
go

select * from Groups
select * from Users
select * from Flights
select * from Roles
select * from Documents
select * from DocumentTypes





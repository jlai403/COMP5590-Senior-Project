use WMS
go

delete from dbo.UserRoles;
delete from dbo.Roles;
delete from dbo.Users;
delete from dbo.Disciplines;
delete from dbo.Faculties;

insert into dbo.Roles 
	values ('Faculty Member')
		,('Faculty Council Member')
		,('Faculty Curriculumn Member')
		,('APPC Member')
		,('GFC Member');

insert into dbo.Faculties
	values ('Bissett School of Business'), ('Science and Technology');

declare @businessId int = (select id from dbo.Faculties where Name = 'Bissett School of Business')
declare @scienceId int = (select id from dbo.Faculties where Name = 'Science and Technology')

insert into dbo.Disciplines (Code, Name, Faculty_Id)
	values ('MKTG','Marketing',@businessId)
		,('MGMT','Management',@businessId)
		,('COMP','Computer Science',@scienceId);

insert into dbo.Semesters ([Year], Term)
	values ('2014','Fall')
		,('2015','Winter')
		,('2015','Spring')
		,('2015','Fall')
		,('2016','Winter')
		,('2015','Spring')
		,('2015','Fall');
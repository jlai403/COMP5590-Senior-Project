using System.EnterpriseServices;
using System.Linq;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Roles;
using WorkflowManagementSystem.Models.Semesters;

namespace WorkflowManagementSystem.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkflowManagementSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WorkflowManagementSystem.WorkflowManagementSystemDbContext";
        }

        protected override void Seed(WorkflowManagementSystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            // ROLES
            if( !FacadeFactory.GetDomainFacade().FindAllRoles().Exists(x => x.Name.Equals("Faculty Member")))
                FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("Faculty Member"));
            if (!FacadeFactory.GetDomainFacade().FindAllRoles().Exists(x => x.Name.Equals("Faculty Council Member")))
                FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("Faculty Council Member"));
            if (!FacadeFactory.GetDomainFacade().FindAllRoles().Exists(x => x.Name.Equals("Faculty Curriculumn Member")))
                FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("Faculty Curriculumn Member"));
            if (!FacadeFactory.GetDomainFacade().FindAllRoles().Exists(x => x.Name.Equals("APPC Member")))
                FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("APPC Member"));
            if (!FacadeFactory.GetDomainFacade().FindAllRoles().Exists(x => x.Name.Equals("GFC Member")))
                FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("GFC Member"));

            // FACULTIES
            if (FacadeFactory.GetDomainFacade().FindFaculty("Bissett School of Business") == null)
                FacadeFactory.GetDomainFacade().CreateFaculty(new FacultyInputViewModel { Name = "Bissett School of Business" });
            if (FacadeFactory.GetDomainFacade().FindFaculty("Science and Technology") == null)
                FacadeFactory.GetDomainFacade().CreateFaculty(new FacultyInputViewModel { Name = "Science and Technology" });

            // DISCIPLINES
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("ACCT")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "ACCT", Faculty = "Bissett School of Business", Name = "Accounting" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("HRES")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "HRES", Faculty = "Bissett School of Business", Name = "Human Resources" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("INBU")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "INBU", Faculty = "Bissett School of Business", Name = "International Business" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("MGMT")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "MGMT", Faculty = "Bissett School of Business", Name = "Management" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("MKTG")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "MKTG", Faculty = "Bissett School of Business", Name = "Marketing" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("BIOL")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "BIOL", Faculty = "Science and Technology", Name = "Biology" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("CHEM")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "CHEM", Faculty = "Science and Technology", Name = "Chemistry" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("COMP")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "COMP", Faculty = "Science and Technology", Name = "Computer Science" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("MATH")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "MATH", Faculty = "Science and Technology", Name = "Math" });
            if (!FacadeFactory.GetDomainFacade().FindAllDisciplines().Exists(x => x.Code.Equals("ACCT")))
                FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "PHYS", Faculty = "Science and Technology", Name = "Physics" });

            // SEMESTERS
            if (!FacadeFactory.GetDomainFacade().FindAllSemesters().Exists(x => x.Term.Equals("Fall") && x.Year.Equals("2014")))
                FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Fall", Year = "2014" });
            if (!FacadeFactory.GetDomainFacade().FindAllSemesters().Exists(x => x.Term.Equals("Winter") && x.Year.Equals("2015")))
                FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Winter", Year = "2015" });
            if (!FacadeFactory.GetDomainFacade().FindAllSemesters().Exists(x => x.Term.Equals("Spring") && x.Year.Equals("2015")))
                FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Spring", Year = "2015" });
            if (!FacadeFactory.GetDomainFacade().FindAllSemesters().Exists(x => x.Term.Equals("Fall") && x.Year.Equals("2015")))
                FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Fall", Year = "2015" });
            if (!FacadeFactory.GetDomainFacade().FindAllSemesters().Exists(x => x.Term.Equals("Winter") && x.Year.Equals("2016")))
                FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Winter", Year = "2016" });
            if (!FacadeFactory.GetDomainFacade().FindAllSemesters().Exists(x => x.Term.Equals("Spring") && x.Year.Equals("2016")))
                FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Spring", Year = "2016" });

            // APPROVAL CHAINS
            if (!FacadeFactory.GetDomainFacade().FindAllApprovalChains().Exists(x => x.Name.Equals("Program")))
                FacadeFactory.GetDomainFacade().CreateApprovalChain(
                    new ApprovalChainInputViewModel { Name = "Program", Roles = {"Faculty Council Member","Faculty Curriculumn Member","APPC Member", "GFC Member"} }
            );
        }
    }
}

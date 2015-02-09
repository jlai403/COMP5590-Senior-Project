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
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("Faculty Member"));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("Faculty Council Member"));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("Faculty Curriculumn Member"));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("APPC Member"));
            FacadeFactory.GetDomainFacade().CreateRole(new RoleInputViewModel("GFC Member"));

            // FACULTIES
            FacadeFactory.GetDomainFacade().CreateFaculty(new FacultyInputViewModel { Name = "Bissett School of Business" });
            FacadeFactory.GetDomainFacade().CreateFaculty(new FacultyInputViewModel { Name = "Science and Technology" });

            // DISCIPLINES
            FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "MKTG", Faculty = "Bissett School of Business", Name = "Marketing" });
            FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel { Code = "MGMT", Faculty = "Bissett School of Business", Name = "Management" });
            FacadeFactory.GetDomainFacade().CreateDiscipline(new DisciplineInputViewModel{ Code = "COMP", Faculty = "Science and Technology", Name = "Computer Science" });

            // SEMESTERS
            FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel { Term = "Fall", Year = "2014" });
            FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel{ Term = "Winter", Year = "2015"});
            FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel{ Term = "Spring", Year = "2015"});
            FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel{ Term = "Fall", Year = "2015"});
            FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel{ Term = "Winter", Year = "2016"});
            FacadeFactory.GetDomainFacade().CreateSemester(new SemesterInputViewModel{ Term = "Spring", Year = "2016"});

            // APPROVAL CHAINS
            FacadeFactory.GetDomainFacade().CreateApprovalChain(
                new ApprovalChainInputViewModel { Name = "Program", Roles = {"Faculty Council Member","Faculty Curriculumn Member","APPC Member", "GFC Member"} }
            );
        }
    }
}

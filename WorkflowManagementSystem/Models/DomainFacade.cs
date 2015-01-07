using System.Collections.Generic;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Roles;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using TransactionHandler = WorkflowManagementSystem.Models.DataAccess.TransactionHandler;

namespace WorkflowManagementSystem.Models
{
    public class DomainFacade
    {
        public List<UserViewModel> FindAllUsers()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var users = UserRepository.FindAllUsers();
                return UserAssembler.AssembleAll(users);
            });
        }

        public void CreateUser(UserSignUpViewModel userSignUpViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                UserRepository.CreateUser(userSignUpViewModel);
                return null;
            });
            
            SecurityManager.CreateAccount(userSignUpViewModel.Email, userSignUpViewModel.Password);
        }

        public void CreateRole(RoleInputViewModel roleInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                RoleRepository.CreateRole(roleInputViewModel);
                return null;
            });
        }

        public List<RoleViewModel> FindAllRoles()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var roles = RoleRepository.FindAllRoles();
                return RoleAssembler.AssembleAll(roles);
            });
        }

        public UserViewModel FindUser(string email)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                return new UserAssembler(user).Assemble();
            });
        }

        public void CreateProgramRequest(string email, ProgramRequestInputViewModel programRequestInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                ProgramRepository.CreateProgram(email, programRequestInputViewModel);
                return null;
            });
        }

        public List<ProgramViewModel> FindAllProgramRequests()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var programs = ProgramRepository.FindAllPrograms();
                return ProgramAssembler.AssembleAll(programs);
            });
        }

        public void CreateFaculty(FacultyInputViewModel facultyInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                FacultyRepository.CreateFaculty(facultyInputViewModel);
                return null;
            });
        }

        public List<FacultyViewModel> FindAllFaculties()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var faculties = FacultyRepository.FindAllFaculties();
                return FacultyAssembler.AssembleAll(faculties);
            });
        }

        public void CreateDiscipline(DisciplineInputViewModel disciplineInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                DisciplineRepository.CreateDiscipline(disciplineInputViewModel);
                return null;
            });
        }

        public List<DisciplineViewModel> FindAllDisciplines()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var disciplines = DisciplineRepository.FindAllDisciplines();
                return DisciplineAssembler.AssembleAll(disciplines);
            });
        }

        public FacultyViewModel FindFaculty(string facultyName)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var faculty = FacultyRepository.FindFaculty(facultyName);
                return new FacultyAssembler(faculty).Assemble();
            });
        }

        public void CreateSemester(SemesterInputViewModel semesterInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                SemesterRepository.CreateSemester(semesterInputViewModel);
                return null;
            });
        }

        public List<SemesterViewModel> FindAllSemesters()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var semesters = SemesterRepository.FindAllSemesters();
                return SemesterAssembler.AssembleAll(semesters);
            });
        }

        public ProgramViewModel FindProgram(string name)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var program = ProgramRepository.FindProgram(name);
                return new ProgramAssembler(program).Assemble();
            });
        }
    }
}
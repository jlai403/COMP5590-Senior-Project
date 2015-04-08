using System;
using System.Collections.Generic;
using FluentAssertions;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Course;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Faculties;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Roles;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models
{
    public class DomainFacade
    {
        public List<UserViewModel> FindAllUsers(bool includeDefaultAdmin = false)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var users = UserRepository.FindAllUsers(includeDefaultAdmin);
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
                var program = ProgramRepository.CreateProgram(email, programRequestInputViewModel);
                new WorkflowHandler(program).InitiateWorkflow();
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
                if (program == null) throw new WMSException("Program '{0}' not found.", name);
                return new ProgramAssembler(program).Assemble();
            });
        }

        public void CreateApprovalChain(ApprovalChainInputViewModel approvalChainInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                ApprovalChainRepository.CreateApprovalChain(approvalChainInputViewModel);
                return null;
            });
        }

        public List<ApprovalChainStepViewModel> FindActiveApprovalChainSteps(string approvalChainType)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var approvalChain = ApprovalChainRepository.FindActiveApprovalChain(approvalChainType);
                return new ApprovalChainAssembler(approvalChain).AssembleApprovalChainSteps();
            });
        }

        public List<ApprovalChainViewModel> FindAllApprovalChains(string approvalChainType)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var approvalChains = ApprovalChainRepository.FindAllApprovalChains(approvalChainType);
                return ApprovalChainAssembler.AssembleAll(approvalChains);
            });
        }

        public void ApproveWorkflowItem(string email, string name, WorkflowItemTypes type)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                var workflowItem = WorkflowRepository.FindWorkflowItemForType(type, name);
                new WorkflowHandler(workflowItem).Approve(user);
                return null;
            });
        }

        public void RejectWorkflowItem(string email, string name, WorkflowItemTypes type)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                var workflowItem = WorkflowRepository.FindWorkflowItemForType(type, name);
                new WorkflowHandler(workflowItem).Reject(user);
                return null;
            });
        }

        public void CompleteWorkflowItem(string email, string name, WorkflowItemTypes type)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                var workflowItem = WorkflowRepository.FindWorkflowItemForType(type, name);
                new WorkflowHandler(workflowItem).Complete(user);
                return null;
            });
        }

        public bool IsWorkflowItemCurrentlyOnLastWorkflowStep(string name, WorkflowItemTypes type)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var workflowItem = WorkflowRepository.FindWorkflowItemForType(type, name);
                return workflowItem.CurrentWorkflowData.IsLastWorkflowStep();
            });
        }

        public CommentViewModel AddComment(string email, CommentInputViewModel commentInputViewModel, WorkflowItemTypes workflowItemType)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                var addedComment = CommentRepository.AddComment(user, commentInputViewModel, workflowItemType);
                return new CommentAssembler(addedComment).Assemble();
            });
        }

        public void UploadFile(string email, FileInputViewModel fileInputViewModel, WorkflowItemTypes workflowItemType)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                FileRepository.UploadFile(user, fileInputViewModel, workflowItemType);
                return null;
            });
        }

        public FileViewModel FindFile(Guid fileId)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var file = FileRepository.FindFile(fileId);
                return new FileAssembler(file).Assemble();
            });
        }

        public List<WorkflowItemViewModel> FindWorkflowItemsAwaitingForAction(string email)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var actionableWorkflowItems = WorkflowRepository.FindWorkflowItemsAwaitingForAction(email);
                return WorkflowAssembler.AssembleWorkflowItems(actionableWorkflowItems);
            });
        }

        public List<WorkflowItemViewModel> FindWorkflowItemsRequestedByUser(string email)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var actionableWorkflowItems = WorkflowRepository.FindWorkflowItemsRequestedByUser(email);
                return WorkflowAssembler.AssembleWorkflowItems(actionableWorkflowItems);
            });
        }

        public void CreateCourseRequest(string email, CourseRequestInputViewModel courseRequestInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var course = CourseRepository.CreateCourse(email, courseRequestInputViewModel);
                new WorkflowHandler(course).InitiateWorkflow();
                return null;
            });
        }

        public CourseViewModel FindCourse(string name)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var course = CourseRepository.FindCourse(name);
                return new CourseAssembler(course).Assemble();
            });
        }

        public void CreateDefaultAdmin()
        {
            TransactionHandler.Instance.Execute(() =>
            {
                UserRepository.CreateDefaultAdmin();
                return null;
            });
            SecurityManager.CreateAccount(UserConstants.DEFAULT_ADMIN_EMAIL, UserConstants.DEFAULT_ADMIN_PASSWORD);
        }

        public bool IsAdmin(string email)
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                return UserRepository.FindUser(email).IsAdmin;
            });
        }

        public void UpdateIsAdmin(string email, bool isAdmin)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var user = UserRepository.FindUser(email);
                user.UpdateIsAdmin(isAdmin);
                return null;
            });
        }

        public void SetActiveApprovalChain(int approvalChainId)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                var approvalChain = ApprovalChainRepository.FindApprovalChain(approvalChainId);
                approvalChain.SetActive(true);
                return null;
            });
        }
    }
}
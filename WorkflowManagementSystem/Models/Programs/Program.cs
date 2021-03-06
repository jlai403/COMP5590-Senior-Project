using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Faculties;
using WorkflowManagementSystem.Models.Search;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Programs
{
    public class Program : WorkflowItem
    {
        public virtual Faculty Faculty { get; set; }
        public virtual Semester Semester { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        
        public override string APPROVAL_CHAIN_NAME { get { return ApprovalChainTypes.PROGRAM; } }

        public void Update(User user, ProgramRequestInputViewModel programRequestInputViewModel)
        {
            AssertProgramWithNameDoesntAlreadyExist(programRequestInputViewModel.Name);
            
            UpdateWorkflowItem(user, programRequestInputViewModel.Name, programRequestInputViewModel.RequestedDateUTC, WorkflowItemTypes.Program);
            UpdateProgram(programRequestInputViewModel);
            AddComment(programRequestInputViewModel.Comment);
            AssertProgram();

            UpdateInvertedIndex();
        }

        private void AssertProgram()
        {
            AssertNameIsNotNull();
            AssertFacultyIsNotNull();
            AssertCrossImpactIsNotNull();
            AssertStudentImpactIsNotNull();
            AssertLibraryImpactIsNotNull();
            AssertITSImpactIsNotNull();
        }

        private void AddComment(string comment)
        {
            if (comment.IsNullOrWhiteSpace()) return;
            AddComment(Requester, RequestedDateUTC, comment);
        }

        private void UpdateProgram(ProgramRequestInputViewModel programRequestInputViewModel)
        {
            Faculty = FacultyRepository.FindFaculty(programRequestInputViewModel.Faculty);
            Semester = SemesterRepository.FindSemester(programRequestInputViewModel.Semester);
            CrossImpact = programRequestInputViewModel.CrossImpact;
            StudentImpact = programRequestInputViewModel.StudentImpact;
            LibraryImpact = programRequestInputViewModel.LibraryImpact;
            ITSImpact = programRequestInputViewModel.ITSImpact;
        }

        public override void UpdateInvertedIndex()
        {
            DeleteInvertedIndice();
            InvertedIndexRepository.AddIndex(this);
        }

        private void AssertFacultyIsNotNull()
        {
            if (Faculty == null)
                throw new WMSException("Faculty is required.");
        }

        private void AssertNameIsNotNull()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw  new WMSException("Program name is required.");
        }

        private void AssertCrossImpactIsNotNull()
        {
            if (string.IsNullOrWhiteSpace(CrossImpact))
                throw new WMSException("Cross impact is required.");
        }

        private void AssertStudentImpactIsNotNull()
        {
            if (string.IsNullOrWhiteSpace(StudentImpact))
                throw new WMSException("Student impact is required.");
        }

        private void AssertLibraryImpactIsNotNull()
        {
            if (string.IsNullOrWhiteSpace(LibraryImpact))
                throw new WMSException("Library impact is required.");
        }

        private void AssertITSImpactIsNotNull()
        {
            if (string.IsNullOrWhiteSpace(ITSImpact))
                throw new WMSException("ITS impact is required.");
        }

        private void AssertProgramWithNameDoesntAlreadyExist(string name)
        {
            if (ProgramRepository.FindProgram(name) != null)
                throw new WMSException("Program with the name '{0}' already exists.", name);
        }

        public override HashSet<string> ExtractKeys()
        {
            var searchKeys = new HashSet<string>();
            searchKeys.Add(Type.ToString().ToLower());
            searchKeys.UnionWith(Name.ToLower().Split(' '));
            searchKeys.UnionWith(Requester.GetDisplayName().ToLower().Split(' '));
            searchKeys.UnionWith(CrossImpact.ToLower().Split(' '));
            searchKeys.UnionWith(StudentImpact.ToLower().Split(' '));
            searchKeys.UnionWith(LibraryImpact.ToLower().Split(' '));
            searchKeys.UnionWith(ITSImpact.ToLower().Split(' '));
            return searchKeys;
        }
    }
}
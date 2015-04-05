using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Search;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Course
{
    public class Course : WorkflowItem
    {
        public override string APPROVAL_CHAIN_NAME { get { return ApprovalChainTypes.COURSE; } }

        public virtual Semester Semester { get; set; }
        public virtual Discipline Discipline { get; set; }
        public int CourseNumber { get; set; }
        public string Credits { get; set; }
        public string Grading { get; set; }
        public string CalendarEntry { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public virtual Program Program { get; set; }
        public virtual List<PrerequisiteCourse> PrerequisiteCourses { get; set; }

        public Course()
        {
            PrerequisiteCourses = new List<PrerequisiteCourse>();
        }

        public void Update(User user, CourseRequestInputViewModel courseRequestInputViewModel)
        {
            UpdateWorkflowItem(user, courseRequestInputViewModel.Name, courseRequestInputViewModel.RequestedDateUTC, WorkflowItemTypes.Course);
            Semester = SemesterRepository.FindSemester(courseRequestInputViewModel.Semester);
            Discipline = DisciplineRepository.FindDiscipline(courseRequestInputViewModel.Discipline);
            CourseNumber = GenerateValidCourseNumber(courseRequestInputViewModel.CourseNumber);
            Credits = courseRequestInputViewModel.Credits;
            Grading = courseRequestInputViewModel.Grading;
            CalendarEntry = courseRequestInputViewModel.CalendarEntry;
            CrossImpact = courseRequestInputViewModel.CrossImpact;
            StudentImpact = courseRequestInputViewModel.StudentImpact;
            LibraryImpact = courseRequestInputViewModel.LibraryImpact;
            ITSImpact = courseRequestInputViewModel.ITSImpact;
            
            UpdateProgram(courseRequestInputViewModel.ProgramName);
            
            UpdatePrerequisites(courseRequestInputViewModel.Prerequisites);

            if (!courseRequestInputViewModel.Comment.IsNullOrWhiteSpace())
            {
                AddComment(user, courseRequestInputViewModel.RequestedDateUTC, courseRequestInputViewModel.Comment);
            }

            UpdateInvertedIndex();
        }

        public override void UpdateInvertedIndex()
        {
            DeleteInvertedIndice();
            InvertedIndexRepository.AddIndex(this);
        }

        public override HashSet<string> ExtractKeys()
        {
            var searchKeys = new HashSet<string>();
            searchKeys.Add(Type.ToString().ToLower());
            searchKeys.Add(CourseNumber.ToString().ToLower());
            searchKeys.Add(Grading.ToLower());
            searchKeys.UnionWith(Name.ToLower().Split(' '));
            searchKeys.UnionWith(Requester.GetDisplayName().ToLower().Split(' '));
            searchKeys.UnionWith(CalendarEntry.ToLower().Split(' '));
            searchKeys.UnionWith(CrossImpact.ToLower().Split(' '));
            searchKeys.UnionWith(StudentImpact.ToLower().Split(' '));
            searchKeys.UnionWith(LibraryImpact.ToLower().Split(' '));
            searchKeys.UnionWith(ITSImpact.ToLower().Split(' '));

            if (Program != null)
                searchKeys.UnionWith(Program.Name.ToLower().Split(' '));
            return searchKeys;
        }

        private void UpdateProgram(string programName)
        {
            if (string.IsNullOrWhiteSpace(programName)) return;

            Program = ProgramRepository.FindProgram(programName);
            if (Program == null) 
                throw new WMSException("Could not find the program '{0}'.", programName);
        }

        private void UpdatePrerequisites(List<string> prerequisites)
        {
            foreach (var prerequisite in prerequisites)
            {
                if (prerequisite.IsNullOrWhiteSpace()) continue;
                PrerequisiteCourses.Add(CourseRepository.CreatePrequisiteCourses(this, prerequisite));
            }
        }

        private int GenerateValidCourseNumber(string courseNumber)
        {
            var takenCourseNumbers = CourseRepository.FindAllCourseNumbers(Discipline);
            var courseNumberGenerator = new CourseNumberGenerator(courseNumber, takenCourseNumbers);
            return courseNumberGenerator.GetNextValidCourseNumber();
        }
    }
}
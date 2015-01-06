using MyEntityFramework.Entity;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Programs
{
    public class Program : IEntity
    {
        public User Requester { get; set; }
        public string Name { get; set; }
        public Semester Semester { get; set; }
        public Discipline Discipline { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public string Comment { get; set; }
        public virtual int Id { get; set; }

        public void Update(ProgramRequestInputViewModel programRequestInputViewModel)
        {
            Requester = UserRepository.FindUser(programRequestInputViewModel.Requester);
            Name = programRequestInputViewModel.Name;
            Semester = SemesterRepository.FindSemester(programRequestInputViewModel.Semester);
            Discipline = DisciplineRepository.FindDiscipline(programRequestInputViewModel.Discipline);
            CrossImpact = programRequestInputViewModel.CrossImpact;
            StudentImpact = programRequestInputViewModel.StudentImpact;
            LibraryImpact = programRequestInputViewModel.LibraryImpact;
            ITSImpact = programRequestInputViewModel.ITSImpact;
            Comment = programRequestInputViewModel.Comment;
        }
    }
}
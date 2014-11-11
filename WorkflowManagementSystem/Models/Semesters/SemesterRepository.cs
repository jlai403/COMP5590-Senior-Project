using System.Collections.Generic;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.Semesters
{
    public class SemesterRepository : Repository
    {
        public static void CreateSemester(SemesterInputViewModel semesterInputViewModel)
        {
            var semester = new Semester();
            AddEntity(semester);
            semester.Update(semesterInputViewModel);
        }

        public static List<Semester> FindAllSemesters()
        {
            return FindAll<Semester>();
        }
    }
}
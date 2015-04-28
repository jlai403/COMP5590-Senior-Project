using System.Collections.Generic;
using System.Linq;
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

        public static Semester FindSemester(int semesterId)
        {
            return Queryable<Semester>().FirstOrDefault(x => x.Id == semesterId);
        }

        public static Semester FindSemester(string term, string year)
        {
            return Queryable<Semester>().FirstOrDefault(x => x.Term.Equals(term) && x.Year.Equals(year));
        }
    }
}
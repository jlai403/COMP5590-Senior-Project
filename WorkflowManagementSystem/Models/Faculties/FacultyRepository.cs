using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.Faculties
{
    public class FacultyRepository : Repository
    {
        public static void CreateFaculty(FacultyInputViewModel facultyInputViewModel)
        {
            var faculty = new Faculty();
            AddEntity(faculty);
            faculty.Update(facultyInputViewModel);
        }

        public static List<Faculty> FindAllFaculties()
        {
            return FindAll<Faculty>();
        }

        public static Faculty FindFaculty(string facultyName)
        {
            return Queryable<Faculty>().FirstOrDefault(x => x.Name.Equals(facultyName));
        }
    }
}
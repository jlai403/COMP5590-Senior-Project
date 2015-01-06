using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Faculty
{
    public class DisciplineRepository : Repository
    {
        public static void CreateDiscipline(DisciplineInputViewModel disciplineInputViewModel)
        {
            var faculty = FacultyRepository.FindFaculty(disciplineInputViewModel.Faculty);
            if (faculty == null) throw new WMSException("Cannot find faculty: {0}", disciplineInputViewModel.Faculty);

            var discipline = new Discipline();
            AddEntity(discipline);
            discipline.Update(disciplineInputViewModel, faculty);
        }

        public static List<Discipline> FindAllDisciplines()
        {
            return FindAll<Discipline>();
        }

        public static Discipline FindDiscipline(int disciplineId)
        {
            return Queryable<Discipline>().FirstOrDefault(x => x.Id == disciplineId);
        }
    }
}
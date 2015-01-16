using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Faculty
{
    public class Faculty : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Discipline> Disciplines { get; set; }

        public Faculty()
        {
            Disciplines = new List<Discipline>();
        }

        public void Update(FacultyInputViewModel facultyInputViewModel)
        {
            Name = facultyInputViewModel.Name;

            AssertNameIsValid();
        }

        private void AssertNameIsValid()
        {
            if (Name.IsNullOrWhiteSpace())
                throw new WMSException("Faculty name is required.");
        }
    }
}
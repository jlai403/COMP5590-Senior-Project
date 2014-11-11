using Microsoft.Ajax.Utilities;
using MyEntityFramework.Entity;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Faculty
{
    public class Discipline : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual Faculty Faculty { get; set; }

        public Discipline(){}

        public void Update(DisciplineInputViewModel disciplineInputViewModel, Faculty faculty)
        {
            Code = disciplineInputViewModel.Code;
            Name = disciplineInputViewModel.Name;
            Faculty = faculty;

            AssertCodeIsValid();
            AssertNameIsValid();
        }

        private void AssertNameIsValid()
        {
            if (Name.IsNullOrWhiteSpace())
                throw new WMSException("Discipline name is required.");
        }

        private void AssertCodeIsValid()
        {
            if (Code.IsNullOrWhiteSpace())
                throw new WMSException("Discipline code is required.");
        }

        public string GetDisplayName()
        {
            return string.Format("{0} - {1}", Code, Name);
        }
    }
}
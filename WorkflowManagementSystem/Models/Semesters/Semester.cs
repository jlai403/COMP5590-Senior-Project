using Microsoft.Ajax.Utilities;
using MyEntityFramework.Entity;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Semesters
{
    public class Semester : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Term { get; set; }
        public virtual string Year { get; set; }

        public void Update(SemesterInputViewModel semesterInputViewModel)
        {
            Year = semesterInputViewModel.Year;
            Term = semesterInputViewModel.Term;

            AssertYearIsValid();
            AssertTermIsValid();
        }

        private void AssertTermIsValid()
        {
            if (Term.IsNullOrWhiteSpace())
                 throw new WMSException("Term is required.");
        }

        private void AssertYearIsValid()
        {
            if (Year.IsNullOrWhiteSpace())
                throw new WMSException("Year is required.");
        }
    }
}
using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Semesters;

namespace WorkflowManagementSystem.Tests
{
    public class SemesterTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateSemester()
        {
            // assemble
            var semesterInputViewModel = new SemesterInputViewModel();
            semesterInputViewModel.Year = "2014";
            semesterInputViewModel.Term = "Winter";

            // act
            FacadeFactory.GetDomainFacade().CreateSemester(semesterInputViewModel);

            // assert
            var semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            semesters.Count.ShouldBeEquivalentTo(1);

            var semester = semesters.First();
            semester.Id.Should().BeGreaterThan(0);
            semester.Year.ShouldBeEquivalentTo("2014");
            semester.Term.ShouldBeEquivalentTo("Winter");
        }

        [Test]
        public void CreateSemester_NoYear()
        {
            // assemble
            var semesterInputViewModel = new SemesterInputViewModel();
            semesterInputViewModel.Term = "Winter";

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateSemester(semesterInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Year is required.");
        }

        [Test]
        public void CreateSemester_NoTerm()
        {
            // assemble
            var semesterInputViewModel = new SemesterInputViewModel();
            semesterInputViewModel.Year = "2014";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateSemester(semesterInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Term is required.");
        }
    }
}
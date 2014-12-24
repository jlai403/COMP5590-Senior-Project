using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Faculty;

namespace WorkflowManagementSystem.Tests
{
    public class FacultyTest : WorkflowManagementSystemTest
    {

        [Test]
        public void CreateFaculty()
        {
            // assemble
            var facultyInputViewModel = new FacultyInputViewModel();
            facultyInputViewModel.Name = "Bissett School of Business";

            // act
            FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);

            // assert
            var faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
            faculties.Count.ShouldBeEquivalentTo(1);
            
            var faculty = faculties.First();
            faculty.Name.ShouldBeEquivalentTo(facultyInputViewModel.Name);
            faculty.Disciplines.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void CreateFaculty_NoName()
        {
            // assemble
            var facultyInputViewModel = new FacultyInputViewModel();

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Faculty name is required.");
        }

        [Test]
        public void FindFaculty()
        {
            // assemble
            var facultyInputViewModel = new FacultyInputViewModel();
            facultyInputViewModel.Name = "Bissett School of Business";
            FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);

            // act
            var faculty = FacadeFactory.GetDomainFacade().FindFaculty(facultyInputViewModel.Name);

            // assert
            faculty.Name.ShouldBeEquivalentTo(facultyInputViewModel.Name);
            faculty.Disciplines.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void CreateDiscipline()
        {
            // assemble
            var faculty = new FacultyTestHelper().CreateValidFaculty();

            var disciplineInputViewModel = new DisciplineInputViewModel();
            disciplineInputViewModel.Code = "COMP";
            disciplineInputViewModel.Name = "Computer Science";
            disciplineInputViewModel.Faculty = faculty.Name;

            // act
            FacadeFactory.GetDomainFacade().CreateDiscipline(disciplineInputViewModel);

            // assert
            var disciplines = FacadeFactory.GetDomainFacade().FindAllDisciplines();
            disciplines.Count.ShouldBeEquivalentTo(1);

            var discipline = disciplines.First();
            discipline.Code.ShouldBeEquivalentTo(disciplineInputViewModel.Code);
            discipline.Name.ShouldBeEquivalentTo(disciplineInputViewModel.Name);
            discipline.Faculty.ShouldBeEquivalentTo(disciplineInputViewModel.Faculty);
        }

        [Test]
        public void CreateDiscipline_NoCode()
        {
            // assemble
            var faculty = new FacultyTestHelper().CreateValidFaculty();

            var disciplineInputViewModel = new DisciplineInputViewModel();
            disciplineInputViewModel.Name = "Computer Science";
            disciplineInputViewModel.Faculty = faculty.Name;

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateDiscipline(disciplineInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Discipline code is required.");
        }

        [Test]
        public void CreateDiscipline_NoName()
        {
            // assemble
            var faculty = new FacultyTestHelper().CreateValidFaculty();

            var disciplineInputViewModel = new DisciplineInputViewModel();
            disciplineInputViewModel.Code = "COMP";
            disciplineInputViewModel.Faculty = faculty.Name;

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateDiscipline(disciplineInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Discipline name is required.");
        }

        [Test]
        public void CreateDiscipline_NoFaculty()
        {
            // assemble
            var disciplineInputViewModel = new DisciplineInputViewModel();
            disciplineInputViewModel.Code = "COMP";
            disciplineInputViewModel.Name = "Computer Science";
            disciplineInputViewModel.Faculty = "";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateDiscipline(disciplineInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Cannot find faculty: ");
        }


        [Test]
        public void FindFaculty_OneDiscipline()
        {
            // assemble
            var expectedFaculty = new FacultyTestHelper().CreateValidFaculty();

            var disciplineInputViewModel = new DisciplineInputViewModel();
            disciplineInputViewModel.Code = "COMP";
            disciplineInputViewModel.Name = "Computer Science";
            disciplineInputViewModel.Faculty = expectedFaculty.Name;
            FacadeFactory.GetDomainFacade().CreateDiscipline(disciplineInputViewModel);

            // act
            var faculty = FacadeFactory.GetDomainFacade().FindFaculty(expectedFaculty.Name);

            // assert
            faculty.Name.ShouldBeEquivalentTo(expectedFaculty.Name);
            faculty.Disciplines.Count.ShouldBeEquivalentTo(1);

            var discipline = faculty.Disciplines.First();
            discipline.ShouldBeEquivalentTo(disciplineInputViewModel.Code + " - " + disciplineInputViewModel.Name);
        }
    }
}
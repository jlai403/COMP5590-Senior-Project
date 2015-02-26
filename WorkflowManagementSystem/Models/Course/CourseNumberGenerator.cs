using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Course
{
    internal class CourseNumberGenerator
    {
        private List<int> TakenCourseNumbers { get; set; }
        private string CourseNumber { get; set; }

        public CourseNumberGenerator(string courseNumber,List<int> takenCourseNumbers)
        {
            TakenCourseNumbers = takenCourseNumbers;
            CourseNumber = courseNumber;
        }

        public int GetNextValidCourseNumber()
        {
            if (CourseNumber.Contains("x") == false) 
                return Convert.ToInt16(CourseNumber);
            
            var courseNumberBase = Convert.ToInt16(CourseNumber.Replace("x", "0"));
            
            if (TakenCourseNumbers.Count == 0 || !TakenCourseNumbers.Contains(courseNumberBase)) 
                return courseNumberBase;

            if (TakenCourseNumbers.Count == 1 && TakenCourseNumbers.First() == courseNumberBase)
                return courseNumberBase + 1;

            var lowestTakenCourseNumber = TakenCourseNumbers.Where(
                (x, index) =>
                    index + 1 < TakenCourseNumbers.Count &&
                    TakenCourseNumbers[index + 1] - x > 1);
            
            return lowestTakenCourseNumber.Any() ? lowestTakenCourseNumber.Min() + 1 : TakenCourseNumbers.Max() + 1;

            //for (int i = 0; i < TakenCourseNumbers.Count; i++)
            //{
            //    var currentCourseNumber = TakenCourseNumbers[i];
            //    if (i + 1 == TakenCourseNumbers.Count)
            //        return currentCourseNumber + 1;
            //    var nextCourseNumber = TakenCourseNumbers[i + 1];

            //    var courseNumberDelta = nextCourseNumber - currentCourseNumber;
            //    if (courseNumberDelta > 1)
            //        return currentCourseNumber + 1;
            //}

            //throw new WMSException("Could not generate a valid course number");
        }
    }
}
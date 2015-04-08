using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorkflowManagementSystem.Models.Faculties;
using WorkflowManagementSystem.Models.Semesters;

namespace WorkflowManagementSystem.Views
{
    public static class CustomHtmlHelper
    {
        public static IEnumerable<SelectListItem> ConvertToSelectListItem(this HtmlHelper helper, List<SemesterViewModel> semesters)
        {
            return
                semesters.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.DisplayName});
        }

        public static IEnumerable<SelectListItem> ConvertToSelectListItem(this HtmlHelper helper, List<FacultyViewModel> faculties)
        {
            return faculties.Select(x => new SelectListItem{Value = x.Name, Text = x.Name});
        }

        public static IEnumerable<SelectListItem> ConvertToSelectListItem(this HtmlHelper helper, List<DisciplineViewModel> disciplines)
        {
            return
                disciplines.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DisplayName }).OrderBy(x => x.Text);
        }

        public static IEnumerable<SelectListItem> ConvertToSelectListItem(this HtmlHelper helper, string[] list)
        {
            return list.Select(x => new SelectListItem { Value = x, Text = x });
        }
    }
}
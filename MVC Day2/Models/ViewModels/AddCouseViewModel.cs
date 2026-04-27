using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Models.ViewModels
{
    public class AddCourseViewModel
    {

        [Required(ErrorMessage = "U should Enter Course Name")]
        [MinLength(2, ErrorMessage = "Name Should be greater than 2 charahcter"),]
        [UniqueAttribute(ErrorMessage = "name must be unique per Depaermwnt")]
        public string Name { get; set; }

        [Range(50, 100, ErrorMessage = "Course Degree must between 50 and 100")]
        public int Degree { get; set; }

        [Remote(action: "CheckMinDeg", controller: "Course", AdditionalFields = "Degree", ErrorMessage = "Minimun Degree must be less than total degree")]
        public int MinDegree { get; set; }

        [divideAttribute]
        public int Hours { get; set; }

        public int Dept_Id { get; set; }
        public IEnumerable<Department>? departments { get; set; }
    }
}

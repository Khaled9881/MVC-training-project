using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Day2.Models
{
    public class Course
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "U should Enter Course Name")]
        //[MinLength(2, ErrorMessage = "Name Should be greater than 2 charahcter"),]
        //[Unique(ErrorMessage = "name must be unique per Depaermwnt")]
        public string Name { get; set; }
        //[Range(50, 100, ErrorMessage = "Course Degree must between 50 and 100")]

        public int Degree { get; set; }

        //[Remote(action: "CheckMinDeg", controller: "Course", AdditionalFields = "Degree", ErrorMessage = "Minimun Degree must be less than total degree")]
        public int MinDegree { get; set; }
        public int Hours { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department? Department { get; set; }


        public ICollection<CrsResuly> CourseResults { get; set; } = new List<CrsResuly>();

    }
}

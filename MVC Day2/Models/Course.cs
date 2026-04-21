using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Day2.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }


        public ICollection<CrsResuly> CourseResults { get; set; } = new List<CrsResuly>();

    }
}

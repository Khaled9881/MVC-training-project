using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Day2.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Address { get; set; }
        public string? grade { get; set; }

        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public Department Department { get; set; }

        public ICollection<CrsResuly> CourseResults { get; set; } = new List<CrsResuly>();

    }
}

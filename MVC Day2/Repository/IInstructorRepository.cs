using AspNetCoreGeneratedDocument;
using MVC_Day2.Models;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Repository
{
    public interface IInstructorRepository : IRepository<Instructor>
    {
        public AllInstructorsViewModel Get_ALL_Instructor_Relevant_Data(int pageNumber);
        public AllInstructorsViewModel GetInstrucotrCourseDeptNames(int id);
    }
}

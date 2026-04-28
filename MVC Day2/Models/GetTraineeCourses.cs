using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Models
{
    public class GetTraineeCourses
    {
        private readonly SchoolContext? SchoolContext;
        public GetTraineeCoursesViewModelcs GV;

        public GetTraineeCourses(SchoolContext _schoolContext)
        {
            SchoolContext = _schoolContext;
            GV = new GetTraineeCoursesViewModelcs();
        }

        public GetTraineeCoursesViewModelcs? GET(int TraineeId)
        {
            var res = SchoolContext.CrsResults.Where(c => c.TraineeId == TraineeId).Select(c => new
            {
                CourseName = c.Course.Name,
                Degree = c.Degree,
                MinDegree = c.Course.MinDegree
            }).ToList();

            if (res == null)
                return null;



            GV.CourseName = res.Select(c => c.CourseName).ToList();
            GV.Degree = res.Select(c => c.Degree).ToList();
            GV.status = res.Select(c =>
            {
                if (c.Degree < c.MinDegree)
                    return "red";
                else
                    return "green";
            }).ToList();

            return GV;
        }
    }
}

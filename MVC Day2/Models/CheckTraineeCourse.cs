using MVC_Day2.Models.ViewModels;

namespace MVC_Day2.Models
{
    public class CheckTraineeCourse
    {
        public TraineeCourseViewModel tv;
        private readonly SchoolContext schoolContext;

        public CheckTraineeCourse(SchoolContext _schoolContext)
        {
            schoolContext = _schoolContext;
            tv = new TraineeCourseViewModel();
        }


        public TraineeCourseViewModel? GetVM(int TraineeID, int CrsId)
        {
            var res = schoolContext.CrsResults.Where(crs => crs.CourseId == CrsId && crs.TraineeId == TraineeID).Select(crs => new
            {
                TraineeName = crs.Trainee.Name,
                CourseName = crs.Course.Name,
                MinDegree = crs.Course.MinDegree,
                Degree = crs.Degree,
            }).FirstOrDefault();

            if (res == null)
                return null;

            tv.TraineeName = res.TraineeName;
            tv.CourseName = res.CourseName;
            tv.Degree = res.Degree;
            tv.Status = res.Degree < res.MinDegree ? "Fail" : "Success";

            return tv;
        }


    }
}

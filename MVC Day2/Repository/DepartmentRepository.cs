using MVC_Day2.Models;

namespace MVC_Day2.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SchoolContext context;

        public DepartmentRepository(SchoolContext _context)
        {
            context = _context;
        }

        //Implementation
        public IEnumerable<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Department entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }



        public int Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Department entity)
        {
            throw new NotImplementedException();
        }
    }
}

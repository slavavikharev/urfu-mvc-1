using System;
using System.Linq;
using URFU_MVC_1.Context;
using URFU_MVC_1.Interfaces;
using URFU_MVC_1.Models;

namespace URFU_MVC_1.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly StudentAppContext _context;

        public StudentsRepository(StudentAppContext context)
        {
            _context = context;
        }

        public void Add(StudentModel student)
        {
            _context.Add(student);
        }

        public void Remove(StudentModel student)
        {
            _context.Remove(student);
        }

        public IQueryable<StudentModel> Get()
        {
            return _context.Students;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}

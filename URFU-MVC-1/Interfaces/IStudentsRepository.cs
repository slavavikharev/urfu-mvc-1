using System;
using System.Linq;
using URFU_MVC_1.Models;

namespace URFU_MVC_1.Interfaces
{
    public interface IStudentsRepository
    {
        void Add(StudentModel student);

        void Remove(StudentModel student);

        IQueryable<StudentModel> Get();

        int SaveChanges();
    }
}

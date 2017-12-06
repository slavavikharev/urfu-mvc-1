using System;
using Microsoft.EntityFrameworkCore;
using URFU_MVC_1.Models;

namespace URFU_MVC_1.Context
{
    public class StudentAppContext : DbContext
    {
        public StudentAppContext(DbContextOptions<StudentAppContext> options)
            : base(options)
        {
        }

        public DbSet<StudentModel> Students { get; set; }
    }
}

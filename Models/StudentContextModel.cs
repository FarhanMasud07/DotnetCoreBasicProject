using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PRD_High_School.Models
{
    public class StudentContextModel : IdentityDbContext
    {
        public StudentContextModel(DbContextOptions<StudentContextModel> options) : base(options)
        {

        }
        public DbSet<StudentModel> StudentItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Seed();
        }
    }
}

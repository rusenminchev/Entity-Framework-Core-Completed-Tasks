namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Configurations;
    using P01_StudentSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //When Fluent API starts getting too fat we create consiguration classes where we place the
            //mapping settings of everyentity

            //builder.ApplyConfiguration(new StudentConfiguration());
            //builder.ApplyConfiguration(new CourseCofinguration());
            //builder.ApplyConfiguration(new ResourceConfiguration());
            //builder.ApplyConfiguration(new HomeworkConsiguration());
            //builder.ApplyConfiguration(new StudentCourseConfiguration());

            //We can do all of this above on one like with a bit of Reflection.
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}

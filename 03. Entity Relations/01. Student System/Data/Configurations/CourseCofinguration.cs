namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    class CourseCofinguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course.HasKey(c => c.CourseId);

            course.Property(c => c.Name)
            .HasMaxLength(80)
            .IsUnicode(true)
            .IsRequired(true);

            course.Property(c => c.Description)
            .HasMaxLength(80)
            .IsUnicode(true)
            .IsRequired(false);

            course.Property(c => c.StartDate)
           .IsRequired(true);

            course.Property(c => c.EndDate)
           .IsRequired(true);

            course.Property(c => c.Price)
           .IsRequired(true);
        }
    }
}

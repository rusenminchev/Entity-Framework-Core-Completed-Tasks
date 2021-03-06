namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student.HasKey(s => s.StudentId);

            student.Property(s => s.Name)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired(true);

            student.Property(s => s.PhoneNumber)
         .HasColumnType("CHAR(10)")
         .IsUnicode(false)
         .IsRequired(false);

            student.Property(s => s.RegisteredOn)
        .IsRequired(true);

            student.Property(s => s.Birthday)
        .IsRequired(false);
        }
    }
}

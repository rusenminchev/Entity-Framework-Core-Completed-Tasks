namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    class HomeworkConsiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> homework)
        {
            homework.HasKey(h => h.HomeworkId);

            homework.Property(h => h.Content)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired(true);

            homework.Property(h => h.SubmissionTime)
            .IsRequired(true);

            homework.HasOne(h => h.Student)
            .WithMany(s => s.Homeworks)
            .HasForeignKey(h => h.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

            homework.HasOne(h => h.Course)
            .WithMany(c => c.Homeworks)
            .HasForeignKey(h => h.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

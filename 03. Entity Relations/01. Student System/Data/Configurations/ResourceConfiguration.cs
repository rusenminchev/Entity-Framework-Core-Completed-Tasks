namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> resourse)
        {
            resourse.HasKey(r => r.ResourceId);

            resourse.Property(r => r.Name)
            .HasMaxLength(50)
            .IsUnicode(true)
            .IsRequired(true);

            resourse.Property(r => r.Url)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired(true);

            resourse.HasOne(r => r.Course)
            .WithMany(c => c.Resources)
            .HasForeignKey(r => r.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

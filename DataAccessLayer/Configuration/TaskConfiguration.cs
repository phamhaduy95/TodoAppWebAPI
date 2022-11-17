using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    internal class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.TaskId);
            builder.HasOne(t => t.User).WithMany(u => u.TaskEntities).HasForeignKey(t => t.UserId).HasConstraintName("FK_task_user").IsRequired(true);
            builder.HasOne(t => t.Category).WithMany(u => u.tasks).HasForeignKey(t => t.CategoryId).HasConstraintName("FK_task_category").OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(t => t.CategoryId).IsRequired(false);
            builder.Property(t => t.Title).HasMaxLength(50);
            builder.Property(t => t.Description).HasMaxLength(150).IsRequired(false);
        }
    }
}
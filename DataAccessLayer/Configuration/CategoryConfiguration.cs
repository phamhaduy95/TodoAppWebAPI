using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.Name).HasMaxLength(100);
            builder.HasOne(c => c.User).WithMany(u => u.Categories).HasForeignKey(c => c.UserId).HasConstraintName("FK_category_user");
            builder.Property(c => c.Description).HasMaxLength(250);
        }
    }
}
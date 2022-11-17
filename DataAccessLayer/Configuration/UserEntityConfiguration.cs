using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(u => u.Organization).HasMaxLength(100).IsRequired(false);
            builder.Property(u => u.Address).HasMaxLength(200).IsRequired(false);
            builder.Property(u => u.FirstName).HasMaxLength(60).IsRequired(false); 
            builder.Property(u => u.LastName).HasMaxLength(60).IsRequired(false);
            builder.Property(u => u.DisplayName).HasMaxLength(60).IsRequired(false);
        }
    }
}
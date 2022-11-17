using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entity
{
    public class UserEntity : IdentityUser<Guid>
    {
        public UserEntity()
        {
            TaskEntities = new List<TaskEntity>();
            Categories = new List<CategoryEntity>();
        }

        public DateTimeOffset? JoinDate { get; set; }
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Organization { get; set; }
        public string? Address { get; set; }
        public ICollection<CategoryEntity> Categories { get; set; }
        public ICollection<TaskEntity> TaskEntities { get; set; }
    }
}
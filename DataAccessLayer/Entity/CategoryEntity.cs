namespace DataAccessLayer.Entity
{
    public class CategoryEntity
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<TaskEntity> tasks { get; set; }
    }
}
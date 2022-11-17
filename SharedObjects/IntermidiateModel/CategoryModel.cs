namespace SharedObjects.IntermidiateModel
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public Guid? UserId { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
    }
}
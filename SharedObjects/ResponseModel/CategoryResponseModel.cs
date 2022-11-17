using SharedObjects.IntermidiateModel;

namespace SharedObjects.ResponseModel
{
    public class CategoryResponseModel
    {
        public CategoryResponseModel(CategoryModel model)
        {
            CategoryId = model.CategoryId;
            Name = model.Name;
            Color = model.Color;
            Description = model.Description;
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
}
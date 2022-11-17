using SharedObjects.Common;
using SharedObjects.IntermidiateModel;

namespace BussinessLayer.Service.Interface
{
    public interface ICategoryService
    {
        public Task<CategoryModel> GetCategoryAsync(Guid CatergoryId);

        public Task<ICollection<CategoryModel>> GetAllAsync();

        public Task<ICollection<CategoryModel>> GetAllForUserAsync(Guid userId);

        public Task<ResponseResult> AddNewCategoryAsync(CategoryModel category);

        public Task<ResponseResult> RemoveCategoryAsync(Guid CatergoryId);

        public Task<ResponseResult> UpdateCategoryAsync(CategoryModel category);
    }
}
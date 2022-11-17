using BussinessLayer.Service.Interface;
using DataAccessLayer.EF;
using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using SharedObjects.Common;
using SharedObjects.IntermidiateModel;
using SharedObjects.Utils;

namespace BussinessLayer.Service.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _appDbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<ResponseResult> AddNewCategoryAsync(CategoryModel model)
        {
            var catergoryToAdd = new CategoryEntity();
            ValueExtractor.ExtractValueFromModelToEntity(catergoryToAdd, model);
            _appDbContext.Categories.Add(catergoryToAdd);
            var result = await _appDbContext.SaveChangesAsync();
            if (result == 0) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ICollection<CategoryModel>> GetAllAsync()
        {
            var list = await (from c in _appDbContext.Categories
                              select new CategoryModel
                              {
                                  CategoryId = c.CategoryId,
                                  Name = c.Name,
                                  UserId = c.UserId,
                                  Description = c.Description,
                                  Color = c.Color
                              }
                              ).ToListAsync();
            return list;
        }

        public async Task<ICollection<CategoryModel>> GetAllForUserAsync(Guid userId)
        {
            var list = await (from c in _appDbContext.Categories
                              where c.UserId == userId
                              select new CategoryModel
                              {
                                  CategoryId = c.CategoryId,
                                  Name = c.Name,
                                  UserId = userId,
                                  Description = c.Description,
                                  Color = c.Color
                              }).ToListAsync();
            return list;
        }

        public async Task<CategoryModel?> GetCategoryAsync(Guid CatergoryId)
        {
            var category = await (from c in _appDbContext.Categories
                                  where c.CategoryId == CatergoryId
                                  select new CategoryModel
                                  {
                                      CategoryId = c.CategoryId,
                                      Name = c.Name,
                                      UserId = c.UserId,
                                      Description = c.Description,
                                      Color = c.Color
                                  }).FirstOrDefaultAsync();
            return category;
        }

        public async Task<ResponseResult> RemoveCategoryAsync(Guid CatergoryId)
        {
            var category = await (from c in _appDbContext.Categories where c.CategoryId == CatergoryId select c).FirstOrDefaultAsync();
            if (category == null) return ResponseResult.NotFound(new { CatergoryId = "not found" });
            _appDbContext.Categories.Remove(category);
            var result = await _appDbContext.SaveChangesAsync();
            if (result == 0) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }

        public async Task<ResponseResult> UpdateCategoryAsync(CategoryModel model)
        {
            var category = await GetCategoryAsync(model.CategoryId);
            if (category == null) return ResponseResult.NotFound(new { CategoryId = "not found" });
            var categoryEntity = new CategoryEntity();
            ValueExtractor.ExtractValueFromModelToEntity(categoryEntity, category);
            ValueExtractor.ExtractValueFromModelToEntity(categoryEntity, model);
            _appDbContext.Categories.Update(categoryEntity);
            var result = await _appDbContext.SaveChangesAsync();
            if (result == 0) return ResponseResult.DataBaseError();
            return ResponseResult.GetSuccessResult();
        }
    }
}
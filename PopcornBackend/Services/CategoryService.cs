using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MajorProjectDbContext _Context;

        public CategoryService(MajorProjectDbContext context)
        {
            this._Context= context;
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            List<CategoryDto> categories = new List<CategoryDto>();            

            foreach(MediaCategory category in this._Context.MediaCategories)
            {
                CategoryDto currentCategory = new CategoryDto();
                currentCategory.CategoryId = category.CategoryId;
                currentCategory.CategoryName = category.CategoryName;   

                categories.Add(currentCategory);
            }
            return categories;
        }
    }
}

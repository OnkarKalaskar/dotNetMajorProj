using PopcornBackend.DTO;
using PopcornBackend.Models;

namespace PopcornBackend.Services
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryDto> GetCategories();
    }
}

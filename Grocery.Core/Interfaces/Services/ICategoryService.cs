using Grocery.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grocery.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
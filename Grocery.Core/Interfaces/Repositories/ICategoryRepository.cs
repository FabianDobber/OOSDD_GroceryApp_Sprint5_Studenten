using Grocery.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetAsync(int id);
        Task<Category?> GetAsync(string name);
    }
}
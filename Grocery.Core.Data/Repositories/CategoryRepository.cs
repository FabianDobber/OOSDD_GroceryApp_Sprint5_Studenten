// In bestand: Grocery.Core/Data/Repositories/CategoryRepository.cs

using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Core.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new List<Category>
        {
            new Category(1, "Groenten"),
            new Category(2, "Fruit"),
            new Category(3, "Zuivel"),
            new Category(4, "Vlees & Vis")
        };

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_categories.AsEnumerable());
        }

        public Task<Category?> GetAsync(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(category);
        }

        public Task<Category?> GetAsync(string name)
        {
            var category = _categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(category);
        }
    }
}
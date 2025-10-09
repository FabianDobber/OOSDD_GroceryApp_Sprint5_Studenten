using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Core.Data.Repositories
{

    //nep database om Interface te implementeren (anders breekt de build)
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly List<ProductCategory> _productCategories = new List<ProductCategory>
        {
            new ProductCategory(1, "Relatie Appel-Fruit", 1, 2),
            new ProductCategory(2, "Relatie Melk-Zuivel", 2, 3),
            new ProductCategory(3, "Product Vlees-Vlees & Vis", 3, 4)
        };

        public Task<ProductCategory?> Get(int id)
        {
            var pc = _productCategories.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(pc);
        }

        public Task<ProductCategory?> Get(string name)
        {
            var pc = _productCategories.FirstOrDefault(p => p.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(pc);
        }
        public Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(int categoryId)
        {
            var result = _productCategories.Where(pc => pc.CategoryId == categoryId);
            return Task.FromResult(result.AsEnumerable());

        }
        public Task AddRelationAsync(int productId, int categoryId)
        {
            int newId = _productCategories.Any() ? _productCategories.Max(pc => pc.Id) + 1 : 1;

            var newRelation = new ProductCategory(newId, $"Relatie P{productId}-C{categoryId}", productId, categoryId);

            _productCategories.Add(newRelation);

            return Task.CompletedTask;
        }

        public Task RemoveRelationAsync(int productId, int categoryId)
        {
            var relationToRemove = _productCategories.FirstOrDefault(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (relationToRemove != null)
            {
                _productCategories.Remove(relationToRemove);
            }

            return Task.CompletedTask;
        }
    }
}
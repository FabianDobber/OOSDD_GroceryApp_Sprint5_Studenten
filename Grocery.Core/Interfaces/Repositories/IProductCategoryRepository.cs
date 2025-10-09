using Grocery.Core.Models;
using System.Threading.Tasks;

namespace Grocery.Core.Interfaces.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory?> Get(int id);
        Task<ProductCategory?> Get(string name);
        Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(int categoryId);
        Task AddRelationAsync(int productId, int categoryId);
        Task RemoveRelationAsync(int productId, int categoryId);

    }
}
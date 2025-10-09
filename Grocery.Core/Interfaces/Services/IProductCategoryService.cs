using Grocery.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grocery.Core.Interfaces.Services
{
    public interface IProductCategoryService
    {
        Task<(IEnumerable<Product> Assigned, IEnumerable<Product> Available)> GetAssignedAndAvailableProductsAsync(int categoryId);
        Task AssignProductToCategoryAsync(int productId, int categoryId);
        Task RemoveProductFromCategoryAsync(int productId, int categoryId);
    }
}
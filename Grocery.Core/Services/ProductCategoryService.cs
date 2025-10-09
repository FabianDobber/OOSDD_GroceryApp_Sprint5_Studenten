using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<(IEnumerable<Product> Assigned, IEnumerable<Product> Available)> GetAssignedAndAvailableProductsAsync(int categoryId)
        {
            var allProducts = await _productRepository.GetAllAsync();
            var assignedProductRelations = await _productCategoryRepository.GetByCategoryIdAsync(categoryId);
            var assignedProductIds = assignedProductRelations.Select(pc => pc.ProductId).ToHashSet();

            var assignedProducts = new List<Product>();
            var availableProducts = new List<Product>();

            foreach (var product in allProducts)
            {
                if (assignedProductIds.Contains(product.Id))
                {
                    assignedProducts.Add(product);
                }
                else
                {
                    availableProducts.Add(product);
                }
            }
            return (assignedProducts, availableProducts);
        }

        public async Task AssignProductToCategoryAsync(int productId, int categoryId)
        {
            await _productCategoryRepository.AddRelationAsync(productId, categoryId);
        }

        public async Task RemoveProductFromCategoryAsync(int productId, int categoryId)
        {
            await _productCategoryRepository.RemoveRelationAsync(productId, categoryId);
        }
    }
}
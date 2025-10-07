// In bestand: Grocery.App/ViewModels/CategoryDetailViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{

    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class CategoryDetailViewModel : ObservableObject
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;
        private readonly IProductCategoryRepository _productCategoryRepo;

        [ObservableProperty]
        private Category currentCategory;

        [ObservableProperty]
        private string categoryId;

        public ObservableCollection<Product> AssignedProducts { get; } = new();
        public ObservableCollection<Product> AvailableProducts { get; } = new();

        public CategoryDetailViewModel(ICategoryRepository catRepo, IProductRepository prodRepo, IProductCategoryRepository pcRepo)
        {
            _categoryRepo = catRepo;
            _productRepo = prodRepo;
            _productCategoryRepo = pcRepo;
        }

        async partial void OnCategoryIdChanged(string value)
        {
            if (int.TryParse(value, out int id))
            {
                await LoadDataAsync(id);
            }
        }

        private async Task LoadDataAsync(int categoryId)
        {
            CurrentCategory = await _categoryRepo.GetAsync(categoryId);

            var allProducts = await _productRepo.GetAllAsync();


            var allProductCategories = await _productCategoryRepo.GetByCategoryIdAsync(categoryId);
            var assignedProductIds = allProductCategories.Select(pc => pc.ProductId).ToHashSet();

            AssignedProducts.Clear();
            AvailableProducts.Clear();
            foreach (var product in allProducts)
            {
                if (assignedProductIds.Contains(product.Id))
                {
                    AssignedProducts.Add(product);
                }
                else
                {
                    AvailableProducts.Add(product);
                }
            }
        }

        [RelayCommand]
        private async Task AddToCategory(Product product)
        {
            if (product == null) return;

            await _productCategoryRepo.AddRelationAsync(product.Id, CurrentCategory.Id);

            AvailableProducts.Remove(product);
            AssignedProducts.Add(product);

        }

        [RelayCommand]
        private async Task RemoveFromCategory(Product product)
        {
            if (product == null) return;

            await _productCategoryRepo.RemoveRelationAsync(product.Id, CurrentCategory.Id);

            AssignedProducts.Remove(product);
            AvailableProducts.Add(product);

        }
    }
}

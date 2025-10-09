using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services; 
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(CategoryId), "CategoryId")]
    public partial class CategoryDetailViewModel : ObservableObject
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductCategoryService _productCategoryService;

        [ObservableProperty]
        private Category currentCategory;

        [ObservableProperty]
        private string categoryId;

        public ObservableCollection<Product> AssignedProducts { get; } = new();
        public ObservableCollection<Product> AvailableProducts { get; } = new();

        public CategoryDetailViewModel(ICategoryService catService, IProductCategoryService prodCatService)
        {
            _categoryService = catService;
            _productCategoryService = prodCatService;
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
            CurrentCategory = await _categoryService.GetCategoryByIdAsync(categoryId);
            var productSets = await _productCategoryService.GetAssignedAndAvailableProductsAsync(categoryId);

            AssignedProducts.Clear();
            foreach (var p in productSets.Assigned) AssignedProducts.Add(p);

            AvailableProducts.Clear();
            foreach (var p in productSets.Available) AvailableProducts.Add(p);
        }

        [RelayCommand]
        private async Task AddToCategory(Product product)
        {
            if (product == null) return;
            await _productCategoryService.AssignProductToCategoryAsync(product.Id, CurrentCategory.Id);

            AvailableProducts.Remove(product);
            AssignedProducts.Add(product);
        }

        [RelayCommand]
        private async Task RemoveFromCategory(Product product)
        {
            if (product == null) return;
            await _productCategoryService.RemoveProductFromCategoryAsync(product.Id, CurrentCategory.Id);

            AssignedProducts.Remove(product);
            AvailableProducts.Add(product);
        }
    }
}
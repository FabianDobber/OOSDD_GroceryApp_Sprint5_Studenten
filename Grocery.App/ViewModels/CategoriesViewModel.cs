// In bestand: Grocery.App/ViewModels/CategoriesViewModel.cs

using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;
using System.Collections.ObjectModel; // Belangrijk voor collecties die de UI updaten
using System.Threading.Tasks;

namespace Grocery.App.ViewModels
{

    public partial class CategoriesViewModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public ObservableCollection<Category> Categories { get; set; }

        public CategoriesViewModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            Categories = new ObservableCollection<Category>();

            Task task = LoadCategoriesAsync();
        }
        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories != null)
            {
                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
        }
    }
}
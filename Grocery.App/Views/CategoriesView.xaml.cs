using Grocery.App.ViewModels;
using Grocery.Core.Models; 

namespace Grocery.App.Views;

public partial class CategoriesView : ContentPage
{
    public CategoriesView(CategoriesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Category selectedCategory)
            return;

        await Shell.Current.GoToAsync($"{nameof(CategoryDetailView)}?CategoryId={selectedCategory.Id}");

        ((CollectionView)sender).SelectedItem = null;
    }
}
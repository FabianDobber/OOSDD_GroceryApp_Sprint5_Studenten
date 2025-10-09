namespace Grocery.App.Views;

public partial class CategoryDetailView : ContentPage
{
    public CategoryDetailView(ViewModels.CategoryDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
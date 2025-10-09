using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class Product : Model
    {
        [ObservableProperty]
        public int stock;
        public DateOnly ShelfLife { get; set; }

        public decimal Price { get; set; }
        public Product(int id, string name, int stock, DateOnly shelfLife, decimal price) : base(id, name) 
        {
            Stock = stock;
            ShelfLife = shelfLife;
            Price = price;
        }

        public Product(int id, string name, int stock, DateOnly dateOnly) : base(id, name)
        {
            this.stock = stock;
            DateOnly = dateOnly;
        }

        public override string? ToString()
        {
            return $"{Name} - {Stock} op voorraad";
        }
    }
}

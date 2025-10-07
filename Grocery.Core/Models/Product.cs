using CommunityToolkit.Mvvm.ComponentModel;

namespace Grocery.Core.Models
{
    public partial class Product : Model
    {
        [ObservableProperty]
        public int stock;
        public DateOnly ShelfLife { get; set; }
        public object Price { get; }
        public DateOnly DateOnly { get; }

        public Product(int id, string name, int stock) : this (id, name, stock, default) { }

        public Product(int id, string name, int stock, DateOnly shelfLife, int price) : base(id, name)
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

namespace Grocery.Core.Models
{
    public partial class ProductCategory : Model
    {

        public int ProductId { get; set; }
        public int CategoryId { get; set; }



        public ProductCategory(int id, string name, int productId, int categoryId) : base(id, name)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }


}

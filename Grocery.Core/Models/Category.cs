namespace Grocery.Core.Models
{


    public partial class Category : Model
    {

        public Category(int id, string name) : base(id, name)
        {
            Id = id;
            Name = name;
        }
    }

}


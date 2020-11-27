
namespace ProductManager.Models
{
    class Category
    {
        public Category(string name, int amountOfProducts)
        {
            Name = name;
            AmountOfProducts = amountOfProducts;
        }

        public Category(int id, string name, int amountOfProducts)
        {
            Id = id;
            Name = name;
            AmountOfProducts = amountOfProducts;
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public int AmountOfProducts { get; protected set; }

        public override string ToString()
        {
            return $"{Name,-10}                                       {AmountOfProducts}";
        }
    }
}

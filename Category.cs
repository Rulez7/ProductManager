using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager
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

        public int Id { get; }

        public string Name { get; }

        public int AmountOfProducts { get; }

        public override string ToString()
        {
            return $"{Name,-10}                                       {AmountOfProducts}";
        }
    }
}

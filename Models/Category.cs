using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public int? ParentId { get; protected set; }

        public Category ParentCategory { get; protected set; }

        public ICollection<Category> ChildrenCategories { get; protected set; } = new List<Category>();

        [Required]
        [MaxLength(50)]
        public string Name { get; protected set; }

        [Required]
        public int AmountOfProducts { get; protected set; }

        public ICollection<Article> Articles { get; protected set; } = new List<Article>();

        public override string ToString()
        {
            return $"{Name,-10}                                       {AmountOfProducts}";
        }
    }
}

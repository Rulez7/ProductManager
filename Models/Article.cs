using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.Models
{
    class Article
    {
        public Article(string number, string name, string description, int price)
        {
            Number = number;
            Name = name;
            Description = description;
            Price = price;
        }

        public Article(int id, string number, string name, string description, int price)
        {
            Id = id;
            Number = number;
            Name = name;
            Description = description;
            Price = price;
        }

        public int Id { get; protected set; }

        private string number;

        [Required]
        [MaxLength(50)]
        public string Number 
        {
            get { return number; }

            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("An article is required to have a number.");
                }

                number = value;
            }
        }

        private string name;

        [Required]
        [MaxLength(50)]
        public string Name 
        {
            get { return name; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("An article is required to have a name.");
                }

                name = value;
            }
        }

        private string description;

        [Required]
        [MaxLength(50)]
        public string Description 
        {
            get { return description; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("An article is required to have a description.");
                }

                description = value;
            }
        }

        [Required]
        public int Price { get; set; }

        public ICollection<Category> Categories { get; protected set; } = new List<Category>();

        public override string ToString()
        {
            return $"{Id}   {Name}";
        }
    }
}

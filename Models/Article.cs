using System;

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

        public string Name 
        {
            get { return name; }

            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("An article is required to have a name.");
                }

                name = value;
            }
        }

        private string description;

        public string Description 
        {
            get { return description; }

            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("An article is required to have a description.");
                }

                description = value;
            }
        }

        public int Price { get; protected set; }

        public override string ToString()
        {
            return $"{Id}   {Name}";
        }
    }
}

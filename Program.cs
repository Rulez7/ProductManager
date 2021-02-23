using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ProductManager.Data;
using ProductManager.Models;

namespace ProductManager
{
    class Program
    {
        static ProductManagerContext Context = new ProductManagerContext();

        static List<Article> articleList = Context.Articles.ToList();

        static List<Category> categoryList = Context.Categories.ToList();

        static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            Console.CursorVisible = false;

            Console.WriteLine("1. Categories");
            Console.WriteLine("2. Articles");
            Console.WriteLine("3. Exit");

            ConsoleKeyInfo input;

            bool invalidInput = true;

            do
            {
                input = Console.ReadKey(true);

                invalidInput = !(input.Key == (ConsoleKey.D1) ||
                        input.Key == (ConsoleKey.D2) ||
                        input.Key == (ConsoleKey.D3));

            } while (invalidInput);

            Console.Clear();

            switch (input.Key)
            {
                case ConsoleKey.D1:

                    CategoryMenu();

                    break;

                case ConsoleKey.D2:

                    ArticleMenu();

                    break;

                case ConsoleKey.D3:

                    Environment.Exit(0);

                    break;
            }
        }

        private static void CategoryMenu()
        {
            Console.CursorVisible = false;

            Console.WriteLine("1. Add category");
            Console.WriteLine("2. List categories");
            Console.WriteLine("3. Add article to category");
            Console.WriteLine("4. Add category to category");

            ConsoleKeyInfo input;

            bool invalidInput = true;

            do
            {
                input = Console.ReadKey(true);

                invalidInput = !(input.Key == (ConsoleKey.D1) ||
                        input.Key == (ConsoleKey.D2) ||
                        input.Key == (ConsoleKey.D3) ||
                        input.Key == (ConsoleKey.D4) ||
                        input.Key == (ConsoleKey.Escape));

            } while (invalidInput);

            Console.Clear();

            switch (input.Key)
            {
                case ConsoleKey.D1:

                    AddCategory();

                    break;

                case ConsoleKey.D2:

                    ListCategories();

                    break;

                case ConsoleKey.D3:

                    SelectCategoryId();

                    break;

                case ConsoleKey.D4:

                    AddCategoryToCategory();

                    break;

                case ConsoleKey.Escape:

                    MainMenu();

                    break;
            }
        }

        private static void AddCategoryToCategory()
        {
            Console.CursorVisible = true;

            int selectedParentId;
            int selectedChildId;

            int posX = 0;

            Console.WriteLine("ID  Category                                         Total products");
            Console.WriteLine("-------------------------------------------------------------------");

            foreach (Category category in categoryList)
            {
                Console.WriteLine($"{category.Id}   {category.Name,-15}{category.AmountOfProducts,+35}");
                posX++;
            }

            posX = posX + 3;

            Console.WriteLine();
            Console.WriteLine("Parent Category ID: ");
            Console.Write(" Child Category ID: ");

            Console.SetCursorPosition(20, posX);
            selectedParentId = Int32.Parse(Console.ReadLine());

            posX++;

            Console.SetCursorPosition(20, posX);
            selectedChildId = Int32.Parse(Console.ReadLine());

            Console.CursorVisible = false;

            Console.Clear();

            Category parentCategory = null;
            Category childCategory = null;

            categoryList = Context.Categories.ToList();

            foreach (Category category in categoryList)
            {
                if (selectedParentId == category.Id)
                {
                    parentCategory = category;
                }
            }

            foreach (Category category in categoryList)
            {
                if (selectedChildId == category.Id)
                {
                    childCategory = category;
                }
            }

            if (parentCategory != null &&
                    childCategory != null &&
                    selectedParentId != selectedChildId)
            {
                InsertCategoryToCategoryRelation(parentCategory, childCategory);

                Console.WriteLine("Category added to category");

                Thread.Sleep(2000);

                Console.Clear();

                MainMenu();
            }

            Console.WriteLine("Parent category and/or Child category does not exist");

            Thread.Sleep(2000);

            Console.Clear();

            CategoryMenu();
        }

        private static void InsertCategoryToCategoryRelation(Category parentCategory, Category childCategory)
        {
            parentCategory.ChildrenCategories.Add(childCategory);

            Context.SaveChanges();
        }

        private static void SelectCategoryId()
        {
            Console.CursorVisible = true;

            int selectedCategoryId;

            Console.WriteLine("ID  Category                                         Total products");
            Console.WriteLine("-------------------------------------------------------------------");

            foreach (Category category in categoryList)
            {
                Console.WriteLine($"{category.Id}   {category.Name,-15}{category.AmountOfProducts,+35}");
            }

            Console.WriteLine();
            Console.Write("Selected ID> ");

            selectedCategoryId = Int32.Parse(Console.ReadLine());

            Console.CursorVisible = false;

            Console.Clear();

            foreach (Category category in categoryList)
            {
                if (category.Id == selectedCategoryId)
                {
                    Console.WriteLine("Name: " + category.Name);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("[A] Add Product");

                    ConsoleKeyInfo input;

                    bool invalidInput = true;

                    do
                    {
                        input = Console.ReadKey(true);

                        invalidInput = !(input.Key == (ConsoleKey.A) ||
                                input.Key == (ConsoleKey.Escape));

                    } while (invalidInput);

                    Console.Clear();

                    switch (input.Key)
                    {
                        case ConsoleKey.A:

                            SelectProductId(category);

                            break;

                        case ConsoleKey.Escape:

                            SelectCategoryId();

                            break;
                    }
                }
            }

            Console.WriteLine("Category does not exist");

            Thread.Sleep(2000);

            Console.Clear();

            CategoryMenu();
        }

        private static void SelectProductId(Category category)
        {
            Console.CursorVisible = true;

            string searchProduct;
            int selectedProductId;
            bool articleFound = false;

            Console.Write("Search product: ");

            searchProduct = Console.ReadLine();

            Console.Clear();

            foreach (Article article in articleList)
            {
                if (article.Name.Contains(searchProduct))
                {
                    articleFound = true;
                }
            }

            if (articleFound == false)
            {
                Console.WriteLine("Article was not found");

                Thread.Sleep(2000);

                Console.Clear();

                CategoryMenu();
            }

            Console.WriteLine("ID  Name");
            Console.WriteLine("----------------------------------------------------------------");

            foreach (Article article in articleList)
            {
                if (article.Name.Contains(searchProduct))
                {
                    Console.WriteLine(article);
                }
            }

            Console.WriteLine();
            Console.Write("Product ID> ");

            selectedProductId = Int32.Parse(Console.ReadLine());

            Console.CursorVisible = false;

            Console.Clear();

            foreach (Article article in articleList)
            {
                if (article.Id == selectedProductId)
                {
                    InsertProductToCategoryRelation(category, article);

                    Console.WriteLine("Product added to category");

                    Thread.Sleep(2000);

                    Console.Clear();

                    MainMenu();
                }
            }

            Console.WriteLine("Article does not exist");

            Thread.Sleep(2000);

            Console.Clear();

            CategoryMenu();
        }

        private static void InsertProductToCategoryRelation(Category category, Article article)
        {
            article.Categories.Add(category);

            Context.SaveChanges();
        }

        private static void ListCategories()
        {
            Console.WriteLine("Category                                         Total products");
            Console.WriteLine("---------------------------------------------------------------");

            categoryList = Context.Categories.ToList();

            foreach (Category category in categoryList)
            {
                Console.WriteLine($"{category.Name,-15}{category.AmountOfProducts,+35}");
            }

            ConsoleKeyInfo userInput;

            bool invalidInput = true;

            do
            {
                userInput = Console.ReadKey(true);

                invalidInput = !(userInput.Key == (ConsoleKey.Escape));

            } while (invalidInput);

            Console.Clear();

            CategoryMenu();
        }

        private static void AddCategory()
        {
            Console.CursorVisible = true;

            string name;
            int amountOfProducts = 0;

            bool addingCategory = true;

            do
            {
                Console.Write("Name: ");

                name = Console.ReadLine();

                Console.CursorVisible = false;

                Console.WriteLine();
                Console.WriteLine("Is this correct? (Y)es (N)o");

                Category category = new Category(name, amountOfProducts);

                bool userDidNotTypeYorN = true;

                do
                {
                    ConsoleKeyInfo inputYorN = Console.ReadKey(true);

                    if (inputYorN.Key == ConsoleKey.Y)
                    {
                        foreach (Category item in categoryList)
                        {
                            if (item.Name == category.Name)
                            {
                                Console.Clear();

                                Console.WriteLine("Category already exists");
                                Thread.Sleep(2000);

                                Console.Clear();

                                MainMenu();
                            }
                        }

                        Console.Clear();

                        InsertCategory(category);

                        Console.WriteLine("Category saved");
                        Thread.Sleep(2000);

                        Console.Clear();

                        userDidNotTypeYorN = false;
                        addingCategory = false;
                    }
                    else if (inputYorN.Key == ConsoleKey.N)
                    {
                        Console.Clear();

                        userDidNotTypeYorN = false;
                    }

                } while (userDidNotTypeYorN);

            } while (addingCategory);

            MainMenu();
        }

        private static void InsertCategory(Category category)
        {
            Context.Categories.Add(category);

            Context.SaveChanges();
        }

        private static void ArticleMenu()
        {
            Console.CursorVisible = false;

            Console.WriteLine("1. Add article");
            Console.WriteLine("2. Search article");
            Console.WriteLine("3. Exit to main menu");

            ConsoleKeyInfo input;

            bool invalidInput = true;

            do
            {
                input = Console.ReadKey(true);

                invalidInput = !(input.Key == (ConsoleKey.D1) ||
                        input.Key == (ConsoleKey.D2) ||
                        input.Key == (ConsoleKey.D3));

            } while (invalidInput);

            Console.Clear();

            switch (input.Key)
            {
                case ConsoleKey.D1:

                    AddArticle();

                    break;

                case ConsoleKey.D2:

                    SearchArticle();

                    break;

                case ConsoleKey.D3:

                    MainMenu();

                    break;
            }
        }

        private static void SearchArticle()
        {
            Console.CursorVisible = true;

            Console.Write("Article number: ");

            string input = Console.ReadLine();

            Console.Clear();

            Article article = FindArticle(input);

            if (article != null)
            {
                ShowArticleInfo(article);
            }

            Console.WriteLine("Unit not found");

            Thread.Sleep(2000);

            Console.Clear();

            MainMenu();
        }

        private static void ShowArticleInfo(Article article)
        {
            Console.CursorVisible = false;

            Console.WriteLine("     Number: " + article.Number);
            Console.WriteLine("       Name: " + article.Name);
            Console.WriteLine("Description: " + article.Description);
            Console.WriteLine("      Price: " + article.Price);

            Console.WriteLine();
            Console.WriteLine("[E] Edit [D] Delete [Esc] Main menu");

            ConsoleKeyInfo input;

            bool invalidInput = true;

            do
            {
                input = Console.ReadKey(true);

                invalidInput = !(input.Key == (ConsoleKey.Escape) ||
                        input.Key == (ConsoleKey.D) ||
                        input.Key == (ConsoleKey.E));

            } while (invalidInput);

            switch (input.Key)
            {
                case ConsoleKey.Escape:

                    Console.Clear();

                    MainMenu();

                    break;

                case ConsoleKey.D:

                    AskForDeleteArticle(article);

                    break;

                case ConsoleKey.E:

                    AddInfoForEditArticle(article);

                    break;
            }
        }

        private static void AddInfoForEditArticle(Article article)
        {
            Console.Clear();

            Console.CursorVisible = true;

            string name;
            string description;
            int price;

            bool editingArticle = true;

            do
            {
                Console.WriteLine("Article number: " + article.Number);
                Console.WriteLine("          Name: ");
                Console.WriteLine("   Description: ");
                Console.WriteLine("         Price: ");

                Console.SetCursorPosition(16, 1);
                name = Console.ReadLine();

                Console.SetCursorPosition(16, 2);
                description = Console.ReadLine();

                Console.SetCursorPosition(16, 3);
                price = Int32.Parse(Console.ReadLine());

                Console.CursorVisible = false;

                Console.WriteLine();
                Console.WriteLine("Is this correct? (Y)es (N)o");

                Article editedArticle = new Article(article.Number, name, description, price);

                bool userDidNotTypeYorN = true;

                do
                {
                    ConsoleKeyInfo inputYorN = Console.ReadKey(true);

                    if (inputYorN.Key == ConsoleKey.Y)
                    {
                        Console.Clear();

                        EditArticle(editedArticle);

                        Console.WriteLine("Article saved");
                        Thread.Sleep(2000);

                        Console.Clear();

                        userDidNotTypeYorN = false;
                        editingArticle = false;
                    }
                    else if (inputYorN.Key == ConsoleKey.N)
                    {
                        Console.Clear();

                        userDidNotTypeYorN = false;
                    }

                } while (userDidNotTypeYorN);

            } while (editingArticle);

            MainMenu();
        }

        private static void EditArticle(Article articleToEdit)
        {
            Article article = Context.Articles.FirstOrDefault(x => x.Number == articleToEdit.Number);

            article.Name = articleToEdit.Name;

            article.Description = articleToEdit.Description;

            article.Price = articleToEdit.Price;

            Context.SaveChanges();
        }

        private static void AskForDeleteArticle(Article article)
        {
            Console.WriteLine();
            Console.WriteLine("Delete this article? (Y)es (N)o");

            bool incorrectInput = true;

            do
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Y:

                        DeleteArticle(article);

                        incorrectInput = false;

                        break;

                    case ConsoleKey.N:

                        Console.Clear();

                        ShowArticleInfo(article);

                        break;
                }
            } while (incorrectInput);

            Console.Clear();

            Console.WriteLine("Article deleted");

            Thread.Sleep(2000);

            Console.Clear();

            MainMenu();
        }

        private static void DeleteArticle(Article article)
        {
            Context.Articles.Remove(article);

            Context.SaveChanges();
        }

        private static void AddArticle()
        {
            Console.CursorVisible = true;

            string number;
            string name;
            string description;
            int price;

            bool addingArticle = true;

            do
            {
                Console.WriteLine("Article number: ");
                Console.WriteLine("          Name: ");
                Console.WriteLine("   Description: ");
                Console.WriteLine("         Price: ");

                Console.SetCursorPosition(16, 0);
                number = Console.ReadLine();

                Console.SetCursorPosition(16, 1);
                name = Console.ReadLine();

                Console.SetCursorPosition(16, 2);
                description = Console.ReadLine();

                Console.SetCursorPosition(16, 3);
                price = Int32.Parse(Console.ReadLine());

                Console.CursorVisible = false;

                Console.WriteLine();
                Console.WriteLine("Is this correct? (Y)es (N)o");

                Article article = new Article(number, name, description, price);

                bool userDidNotTypeYorN = true;

                do
                {
                    ConsoleKeyInfo inputYorN = Console.ReadKey(true);

                    if (inputYorN.Key == ConsoleKey.Y)
                    {
                        foreach (Article item in articleList)
                        {
                            if (item.Number == article.Number)
                            {
                                Console.Clear();

                                Console.WriteLine("Article already exists");
                                Thread.Sleep(2000);

                                Console.Clear();

                                MainMenu();
                            }
                        }

                        Console.Clear();

                        InsertArticle(article);

                        Console.WriteLine("Article saved");
                        Thread.Sleep(2000);

                        Console.Clear();

                        userDidNotTypeYorN = false;
                        addingArticle = false;
                    }
                    else if (inputYorN.Key == ConsoleKey.N)
                    {
                        Console.Clear();

                        userDidNotTypeYorN = false;
                    }

                } while (userDidNotTypeYorN);

            } while (addingArticle);

            MainMenu();
        }

        private static Article FindArticle(string number)
        {
            Article article = Context.Articles.FirstOrDefault(x => x.Number == number);

            return article;
        }

        private static void InsertArticle(Article article)
        {
            Context.Articles.Add(article);

            Context.SaveChanges();
        }
    }
}

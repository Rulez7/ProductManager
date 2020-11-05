using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace ProductManager
{
    class Program
    {
        static string connectionString = "Server=localhost;Database=ProductManager;Integrated Security=True";
             
        static void Main(string[] args)
        {
            MainMenu();
        }

        private static void MainMenu()
        {
            Console.CursorVisible = false;

            Console.WriteLine("1. Add article");
            Console.WriteLine("2. Search article");
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

                    AddArticle();

                    break;

                case ConsoleKey.D2:

                    SearchArticle();

                    break;

                case ConsoleKey.D3:

                    Environment.Exit(0);

                    break;
            }
        }

        private static void SearchArticle()
        {
            Console.CursorVisible = true;

            Console.Write("Article number: ");

            string input = Console.ReadLine();

            Console.Clear();

            foreach (Article article in FetchArticles())
            {
                if (article.Number == input)
                {
                    ShowArticleInfo(article);
                }
            }

            Console.Clear();

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

                    //TestEditArticle(article);

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
                Console.WriteLine("Name: ");
                Console.WriteLine("Description: ");
                Console.WriteLine("Price: ");

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

        private static void TestEditArticle(Article article)
        {
            var sql = $@"
                UPDATE Article
                SET Name='Mössa', Description='Fin mössa', Price='299'
                WHERE Id=(@Id)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", article.Id);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        private static void EditArticle(Article article)
        {
            var sql = $@"
                UPDATE Article
                SET Name=@Name, Description=@Description, Price=@Price
                WHERE Number=@Number";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@Name", article.Name);
            command.Parameters.AddWithValue("@Description", article.Description);
            command.Parameters.AddWithValue("@Price", article.Price);
            command.Parameters.AddWithValue("@Number", article.Number);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
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
            var sql = $@"
                DELETE FROM Article
                WHERE Id=(@Id)";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", article.Id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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
                Console.WriteLine("Name: ");
                Console.WriteLine("Description: ");
                Console.WriteLine("Price: ");

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
                        foreach (Article item in FetchArticles())
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

        private static List<Article> FetchArticles()
        {
            string sql = "SELECT Id, Number, Name, Description, Price FROM Article";

            List<Article> articleList = new List<Article>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var id = (int)reader["Id"];
                        var number = (string)reader["Number"];
                        var name = (string)reader["Name"];
                        var description = (string)reader["Description"];
                        var price = (int)reader["Price"];

                        articleList.Add(new Article(id, number, name, description, price));
                    }

                    connection.Close();
                }
            }

            return articleList;
        }

        private static void InsertArticle(Article article)
        {
            var sql = $@"
            INSERT INTO Article (Number, Name, Description, Price)
            VALUES (@Number, @Name, @Description, @Price)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Number", article.Number);
            command.Parameters.AddWithValue("@Name", article.Name);
            command.Parameters.AddWithValue("@Description", article.Description);
            command.Parameters.AddWithValue("@Price", article.Price);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using StockApp_Console.Settings.Models;
using System.IO;
using StockApp_Console.Settings;
using ConsoleTables;

namespace StockApp_Console
{
    public class Program
    {
        public AppSettings Settings { get; private set; }
        public static List<string> FileNames { get; private set; }
        static List<Article> Stock = new List<Article>();

        public static Program MainApp = null;

        // Générateur d'id auto (à finir)
        private static int id;
        public static int GenerateId()
        {
            return id++;
        }

        public static void Main(string[] args)
        {
            MainApp = new Program();
            MainApp.MainNoStatic();

            // TESTING PURPOSE
            Stock.Add(new Article(GenerateId(), "test", 10, 5000));
            Stock.Add(new Article(GenerateId(), "test", 40, 1000));
            Stock.Add(new Article(GenerateId(), "test2", 50, 2005));
            Stock.Add(new Article(GenerateId(), "test3", 7, 5074));
            Stock.Add(new Article(GenerateId(), "test4", 46, 4501));

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleMenu.Show("main",
                "Rechercher un article",
                "Ajouter un article au stock en vérifiant l’unicité de la référence",
                "Supprimer un article par référence",
                "Modifier un article par référence",
                "Afficher tous les articles",
                "Paramètres",
                "Quitter la Console");
        }

        private void MainNoStatic()
        {
            CreateFiles();
            ConfigFileManager.LoadConfigFiles(MainApp);
            /*var test = Settings.Database ?? new Dictionary<string, string>();
            test.Add("test", "tast");*/
            Settings.SaveConfig();
        }

        private static void CreateFiles()
        {
            FileNames = new List<string>();
            createFile("settings.json");

            void createFile(string filename)
            {
                FileNames.Add(filename);
                if (!File.Exists(Config.BasePath + filename))
                    File.Create(Config.BasePath + filename).Close();
            }
        }

        public static void SearchMenu()
        {
            Console.Title = "Rechercher | Stock App";

            ConsoleMenu.Show("search",
                "Par référence",
                "Par nom",
                "Par intervalle de prix de vente",
                "Retour");
        }

        public static void SearchByReference()
        {
            Console.Title = "Par référence | Rechercher";
            ConsoleMenu.PageTitle("Rechercher");
            var table = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);

            Console.Write("Référence de l'article: ");
            int searchByNumber;
            bool input = int.TryParse(Console.ReadLine(), out searchByNumber);

            if (input)
            {
                foreach (Article article in Stock)
                {
                    if (article.Number.Equals(searchByNumber))
                    {
                        //ConsoleMenu.DisplayTable(article.Number, article.Name, article.Price, article.Quantity);
                    }
                    else
                    {
                        ConsoleMenu.DisplayMessage("error", "La référence entrée n'extiste pas !");
                    }
                }
            }
            else
            {
                ConsoleMenu.DisplayMessage("error", "La valeur peut seulement être un chiffre, veuillez réessayer.");
                Console.ReadKey();
                SearchByReference();
            }
            Console.ReadKey();
        }

        public static void SearchByName()
        {
            Console.Title = "Par Nom | Rechercher";
            ConsoleMenu.PageTitle("Rechercher");
            var table = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);

            Console.Write("Nom de l'article: ");
            string nameInput = Console.ReadLine();

            for (int i = 0; i < Stock.Count; i++)
            {
                if (Stock[i].Name.Equals(nameInput))
                {
                    //ConsoleMenu.DisplayTable(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
                }
                else
                {
                    ConsoleMenu.DisplayMessage("error", "Erreur WIP.");
                }
            }
            Console.ReadKey();
        }

        public static void SearchByPriceInterval()
        {
            Console.Title = "Par Prix | Rechercher";
            ConsoleMenu.PageTitle("Rechercher");

            Console.Write("Montant minimum: ");
            float startPrice = float.Parse(Console.ReadLine());
            Console.Write("Montant maximum: ");
            float endPrice = float.Parse(Console.ReadLine());
            //boucle for sur le nombre de ligne
            foreach (Article article in Stock)
            { // if prix minimum  entre prix max
                if (article.Price >= (startPrice) && article.Price <= (endPrice))
                {
                    //ConsoleMenu.DisplayTable(article.Number, article.Name, article.Price, article.Quantity);
                }
            }
            Console.ReadKey();
        }

        public static void AddArticle()
        {
            ConsoleMenu.PageTitle("Ajouter");
            var table = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);

            Console.Write("Numéro [0 = auto]: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Nom: ");
            string name = Console.ReadLine();
            Console.Write("Prix: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Quantité: ");
            int quantity = int.Parse(Console.ReadLine());

            bool numberExist = true;
            if (number == 0)
            {
                number = GenerateId();
                Stock.Add(new Article(number, name, price, quantity));

            }
            else
            {
                foreach (Article article in Stock)
                {
                    if (article.Number.Equals(number))
                    {
                        ConsoleMenu.DisplayMessage("error", "Ce numéro existe déjà, veuiller en saisir un nouveau.");
                        numberExist = false;
                    }
                }
                if (numberExist == true)
                {
                    Stock.Add(new Article(number, name, price, quantity));
                    // TEST EXECUTION BDD
                    try 
                    {
                        using (var db = new Database())
                        {
                            db.Connection.Open();
                            using (var cmd = db.Connection.CreateCommand())
                            {
                                cmd.CommandText = @"INSERT INTO article VALUES (number, name, price, quantity)";
                                cmd.ExecuteNonQuery();
                            }
                        }   
                    } 
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //ConsoleMenu.DisplayTable(number, name, price, quantity);
            Console.ReadKey();
        }

        public static void DeleteArticle()
        {
            ConsoleMenu.PageTitle("Supprimer");
            var table = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);

            Console.Write("Référence de l'article à supprimer : ");
            int articleToDeleteById = int.Parse(Console.ReadLine());

            for (int i = 0; i < Stock.Count; i++)
            {
                if (Stock[i].Number.Equals(articleToDeleteById))
                {
                    //ConsoleMenu.DisplayTable(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
                    Stock.RemoveAt(i);

                }
            }
            Console.WriteLine("Vous avez supprimé l'article");
            Console.ReadKey();
        }

        public static void EditArticle()
        {
            ConsoleMenu.PageTitle("Modifier");
            var preview = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);
            var choosen = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);
            var edited = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);

            foreach (Article article in Stock)
            {
                preview.AddRow(article.Number, article.Name, article.Price, article.Quantity);
            }
            preview.Write(Format.Alternative);

            Console.Write("Choisissez l'article à modifier");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" (par numéro) ");
            Console.ResetColor();
            Console.Write(": ");

            int editArticleById = int.Parse(ConsoleMenu.UserInput());
            Console.WriteLine();

            foreach (Article article in Stock)
            {
                // si article est egale à editArticleById il affiche la ligne de l'article
                if (article.Number.Equals(editArticleById))
                {
                    choosen.AddRow(article.Number, article.Name, article.Price, article.Quantity);
                    choosen.Write(Format.Alternative);

                    if (ConsoleMenu.Confirm("Voulez-vous modifier cet article"))
                    {
                        Console.Write("Nouveau numéro : ");
                        int newNumber = int.Parse(Console.ReadLine());

                        Console.Write("Nouveau nom : ");
                        string newName = Console.ReadLine();

                        Console.Write("Nouveau prix : ");
                        float newPrice = float.Parse(Console.ReadLine());

                        Console.Write("Nouvelle quantité : ");
                        int newQuantity = int.Parse(Console.ReadLine());

                        edited.AddRow(newNumber, newName, newPrice, newQuantity);
                        edited.Write(Format.Alternative);

                        if (ConsoleMenu.Confirm("Êtes-vous sûr"))
                        {
                            article.Number = newNumber;
                            article.Name = newName;
                            article.Price = newPrice;
                            article.Quantity = newQuantity;

                            ConsoleMenu.DisplayMessage("success", "Article modifié avec succès !");
                        }
                        else
                        {
                            EditArticle();
                        }
                    }
                }
            }
            Console.ReadKey();
        }

        public static void ShowAll()
        {
            ConsoleMenu.PageTitle("Articles");

            var table = new ConsoleTable(Article.displayNumber, Article.displayName, Article.displayPrice, Article.displayQuantity);

            foreach (Article article in Stock)
            {
                table.AddRow(article.Number, article.Name, article.Price, article.Quantity);
            }
            table.Write(Format.Alternative);
            Console.ReadKey();
        }

        public static void ShowSettings()
        {


        }
    }
}
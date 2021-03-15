using System;
using System.Collections.Generic;
using Figgle;
using StockApp.Settings.Models;
using System.IO;
using StockApp.Settings;
using System.Threading.Tasks;

namespace StockApp
{
    public class Program
    {
        public AppSettings Settings { get; private set; }
        public static List<string> FileNames { get; private set; }
        static List<Article> Stock = new List<Article>();

        public static Program MainApp = null;

        // Générateur d'id auto (à finir)
        private static int id = 1;
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
            ConsoleMenu.Show("main", true,
                "Rechercher un article",
                "Ajouter un article au stock en vérifiant l’unicité de la référence",
                "Supprimer un article par référence",
                "Modifier un article par référence",
                "Afficher tous les articles",
                "Quitter la Console");
        }

        private void MainNoStatic()
        {
            CreateFiles();
            ConfigFileManager.LoadConfigFiles(MainApp);
            var test = Settings.Database ?? new Dictionary<string, string>();
            test.Add("test", "tast");
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
                true,
                "Par référence",
                "Par nom",
                "Par intervalle de prix de vente");
        }

        public static void SearchByReference()
        {
            Console.Title = "Par référence | Rechercher";
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Rechercher"));
            Console.Write("Référence de l'article: ");

            int searchByNumber;
            bool input = int.TryParse(Console.ReadLine(), out searchByNumber);

            if (input)
            {
                for (int i = 0; i < Stock.Count; i++)
                {
                    if (Stock[i].Number.Equals(searchByNumber))
                    {
                        ConsoleMenu.DisplayTable(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
                    }
                }
            }
            else
            {
                ConsoleMenu.DisplayError("La valeur peut seulement être un chiffre, veuillez réessayer.");
            }
            Console.ReadKey();
        }

        public static void SearchByName()
        {
            Console.Title = "Par Nom | Rechercher";
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Rechercher"));
            Console.Write("Nom de l'article: ");

            string nameInput = Console.ReadLine();
            for (int i = 0; i < Stock.Count; i++)
            {
                if (Stock[i].Name.Equals(nameInput))
                {
                    ConsoleMenu.DisplayTable(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
                }
                else
                {
                    ConsoleMenu.DisplayError("Erreur WIP.");
                }
            }
            Console.ReadKey();
        }

        public static void SearchByPriceInterval()
        {
            Console.Title = "Par Prix | Rechercher";
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Rechercher"));

            Console.Write("Montant minimum: ");
            float startPrice = float.Parse(Console.ReadLine());
            Console.Write("Montant maximum: ");
            float endPrice = float.Parse(Console.ReadLine());
            //boucle for sur le nombre de ligne
            foreach (Article article in Stock)
            { // if prix minimum  entre prix max
                if (article.Price >= (startPrice) && article.Price <= (endPrice))
                {
                    ConsoleMenu.DisplayTable(article.Number, article.Name, article.Price, article.Quantity);
                }
            }
            Console.ReadKey();
        }

        public static void AddArticle()
        {
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Ajouter"));
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
                        ConsoleMenu.DisplayError("Ce numéro existe déjà, veuiller en saisir un nouveau.");
                        numberExist = false;
                    }
                }
                if (numberExist == true)
                {
                    Stock.Add(new Article(number, name, price, quantity));
                }
            }
            ConsoleMenu.DisplayTable(number, name, price, quantity);
            Console.ReadKey();

            // TEST EXECUTION BDD
            using (var db = new Database())
            {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand())
                {
                    //cmd.CommandText = @"CREATE TABLE TestTable";
                    //cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteArticle()
        {
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Supprimer"));
            Console.Write("Référence de l'article à supprimer: ");

            int articleToDeleteById = int.Parse(Console.ReadLine());
            for (int i = 0; i < Stock.Count; i++)
            {
                if (Stock[i].Number.Equals(articleToDeleteById))
                {
                    ConsoleMenu.DisplayTable(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
                    Stock.RemoveAt(i);

                }
            }
            Console.WriteLine("Vous avez supprimé l'article");
            Console.ReadKey();
        }

        public static void EditArticle()
        {
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Modifier"));
            Console.Write("Numéro de l'article à modifier: ");
            int editArticleById = int.Parse(Console.ReadLine());
            for (int i = 0; i < Stock.Count; i++)
            {
                // si i est egale à editArticleById il affiche la ligne i
                if (Stock[i].Number.Equals(editArticleById))
                {
                    Console.WriteLine("Voulez-vous modifier cet article : (o/n)");
                    Console.WriteLine(Stock[i].ToString());
                    string reponse = Console.ReadLine();
                    if (reponse == "o")
                    {
                        Console.WriteLine("Entré le nouveau numéro de l'article");
                        int newNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Entré le nouveau nom de l'article");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Entré le nouveau prix de l'article");
                        float newPrice = float.Parse(Console.ReadLine());
                        Console.WriteLine("Entré la nouvelle quantité de l'article");
                        int newQuantity = int.Parse(Console.ReadLine());
                        // Attribution set au article
                        Stock[i].Number = newNumber;
                        Stock[i].Name = newName;
                        Stock[i].Price = newPrice;
                        Stock[i].Quantity = newQuantity;
                        // utilise le set
                    }
                }
            }
            Console.ReadKey();
        }

        public static void ShowAll()
        {
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Articles"));

            foreach (Article article in Stock)
            {
                ConsoleMenu.DisplayTable(article.Number, article.Name, article.Price, article.Quantity);
            }
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using Figgle;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace StockApp
{
    class Program
    {
        public static List<Article> Stock = new List<Article>();

        // CLASSE DE TEST D'EXECUTION DE LA BDD
        public static void SaveToDb()
        {
            using (var db = new Database())
            {
                try
                {
                    db.Connection.Open();
                    using (var cmd = db.Connection.CreateCommand())
                    {
                        cmd.CommandText = @"CREATE TABLE TestTable";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(e);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Une erreur est survenue, veuillez vérifier votre configuration.");
                    Console.ResetColor();
                }

            }
        }

        static void Main(string[] args)
        {
            // TEST EXECUTION BDD
            SaveToDb();
            // TEST SETTINGS EN JSON
            //var jsonString = JsonSerializer.Serialize(Stock);
            //File.WriteAllText("test.json", jsonString);

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // DESACTIVE DURANT TEST
            /*ConsoleMenu.Show("main", true,
                "Rechercher un article",
                "Ajouter un article au stock en vérifiant l’unicité de la référence",
                "Supprimer un article par référence",
                "Modifier un article par référence",
                "Afficher tous les articles",
                "Quitter la Console");*/
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
                Console.WriteLine("[ERREUR] La valeur peut seulement être un chiffre, veuillez réessayer.");
            }

            Console.ReadLine();
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
                    Console.WriteLine("Erreur WIP.");
                }
            }

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
            for (int i = 0; i < Stock.Count; i++)
            { // if prix minimum  entre prix max
                if (Stock[i].Price >= (startPrice) && Stock[i].Price <= (endPrice))
                {
                    ConsoleMenu.DisplayTable(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
                }
            }
            Console.ReadLine();
        }

        public static void AddArticle()
        {
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Ajouter"));
            Console.Write("Numéro: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Nom: ");
            string name = Console.ReadLine();
            Console.Write("Prix: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Quantité: ");
            int quantity = int.Parse(Console.ReadLine());

            bool numberExist = true;
            for (int i = 0; i < Stock.Count; i++)
            {
                if (Stock[i].Number.Equals(number))
                {
                    Console.WriteLine("[ERREUR] Ce numéro existe déjà, veuiller en saisir un nouveau.");
                    numberExist = false;
                }
            }
            if (numberExist == true)
            {
                Stock.Add(new Article(number, name, price, quantity));
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                // TEST SETTINGS EN JSON
                var jsonString = JsonSerializer.Serialize(Stock, options);
                File.WriteAllText("test.json", jsonString);
                // END TEST
                ConsoleMenu.DisplayTable(number, name, price, quantity);
            }
            Console.ReadLine();
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
            Console.ReadLine();
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
            Console.ReadLine();
        }

        public static void ShowAll()
        {
            Console.Clear();
            Console.WriteLine(FiggleFonts.Slant.Render("  Articles"));

            displayTable();
            Console.ReadLine();
        }

        public static void displayTable()
        {
            foreach (Article article in Stock)
            {
                ConsoleMenu.DisplayTable(article.Number, article.Name, article.Price, article.Quantity);
            }
        }
    }
}
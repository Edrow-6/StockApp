using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using ConsoleTables;

namespace StockApp 
{
    class Program 
    {
        public static List<Article> Stock = new List<Article> ();

        static void Main(string[] args) 
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            ConsoleMenu.MainMenu(true,
                "Rechercher un article",
                "Ajouter un article au stock en vérifiant l’unicité de la référence",
                "Supprimer un article par référence",
                "Modifier un article par référence",
                "Afficher tous les articles",
                "Quitter la Console");
        }

        public static void SearchMenu()
        {
            ConsoleMenu.SearchSubMenu(
                "Par référence",
                "Par nom",
                "Par intervalle de prix de vente");
        }

        public static void SearchByReference() 
        {
            Console.Clear();
            Console.WriteLine(Figgle.FiggleFonts.Slant.Render("  Rechercher"));

            int searchByNumber;
            bool input = int.TryParse(Console.ReadLine(), out searchByNumber);

            if (input) {
                // Select * from Article where id_article = searchByNumber
                // i vaut 0 Stock.Count est le nombre de ligne du stock ,  i va maximum jusqu'au nombre limite de ligne
                for (int i = 0; i < Stock.Count; i++) {
                    // si i est egale à searchByNumber il affiche la ligne i
                    if (Stock[i].Number.Equals(searchByNumber)) {
                        Console.WriteLine(Stock[i].ToString());
                    }
                }
            } else {
                Console.WriteLine("[ERREUR] Valeur introuvable ou incorrect, veuillez réessayer.");
            }
            
            Console.ReadLine();
        }

        public static void SearchByName() 
        {
            Console.Clear();
            Console.WriteLine("Rechercher un article par nom.");
            Console.WriteLine("Choisissez le nom de l'article recherché.");
            string nameInput = Console.ReadLine();
            // SELECT * FROM Artcile WHERE nameInput = nameInput
            for (int i = 0; i < Stock.Count; i++) { // if s'execute quand le nom article est egale au nameInput
                if (Stock[i].Name.Equals(nameInput)) {
                    Console.WriteLine(Stock[i].ToString());

                }
            }

            Console.ReadLine();
        }

        public static void SearchByPriceInterval() 
        {
            Console.Clear();
            Console.WriteLine("Rechercher un article par intervalle de prix de vente.");
            Console.WriteLine("Choisissez le prix de depart");
            float startPrice = float.Parse(Console.ReadLine());
            Console.WriteLine("Choisissez le prix de fin");
            float endPrice = float.Parse(Console.ReadLine());
            //boucle for sur le nombre de ligne
            for (int i = 0; i < Stock.Count; i++) { // if prix minimum  entre prix max
                if (Stock[i].Price >= (startPrice) && Stock[i].Price <= (endPrice)) {
                    //affiche les articles dont le prix est entre startPrice et endPrice
                    Console.WriteLine(Stock[i].ToString());

                }
            }
            // SELECT * FROM Article where articlePrice BETWEEN startPrice AND endPrice
            Console.ReadLine();
        }

        public static void AddArticle() 
        {
            Console.Clear();
            Console.WriteLine(Figgle.FiggleFonts.Slant.Render("  Ajouter"));
            Console.Write("Numéro: ");
            int number = int.Parse(Console.ReadLine());
            Console.Write("Nom: ");
            string name = Console.ReadLine();
            Console.Write("Prix: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Quantité: ");
            int quantity = int.Parse(Console.ReadLine());

            bool numberExist = true;
            for (int i = 0; i < Stock.Count; i++) {
                if (Stock[i].Number.Equals(number)) {
                    Console.WriteLine("[Erreur] Ce numéro existe déjà, veuiller en saisir un nouveau.");
                    numberExist = false;
                }
            }
            if (numberExist == true) {
                Stock.Add(new Article(number, name, price, quantity));
                //Console.WriteLine($"[Article Ajouté] NUMÉRO: {number} - NOM: {name} - PRIX: {price} - QUANTITÉ: {quantity}");
                var table = new ConsoleTable("NUMÉRO", "NOM", "PRIX", "QUANTITÉ");
                table.AddRow(number, name, price, quantity);

                table.Write(Format.Alternative);
            }
            Console.ReadLine();
        }

        public static void DeleteArticle() 
        {
            Console.Clear();
            Console.WriteLine(Figgle.FiggleFonts.Slant.Render("  Supprimer"));

            int articleToDeleteById = int.Parse(Console.ReadLine());
            for (int i = 0; i < Stock.Count; i++) {
                if (Stock[i].Number.Equals(articleToDeleteById)) {
                    Console.WriteLine(Stock[i].ToString());
                    //suprimme la ligne i de la list
                    Stock.RemoveAt(i);

                }
            }
            // DELETE from Artcile where id_article= id_del

            Console.WriteLine("Vous avez supprimé l'article");
            Console.ReadLine();
        }

        public static void EditArticle() 
        {
            Console.Clear();
            Console.WriteLine(Figgle.FiggleFonts.Slant.Render("  Modifier"));
            Console.WriteLine("Choisissez l'id de l'article à modifier.");
            int editArticleById = int.Parse(Console.ReadLine());
            for (int i = 0; i < Stock.Count; i++) {
                // si i est egale à editArticleById il affiche la ligne i
                if (Stock[i].Number.Equals(editArticleById)) {
                    Console.WriteLine("Voulez-vous modifier cet article : (o/n)");
                    Console.WriteLine(Stock[i].ToString());
                    string reponse = Console.ReadLine();
                    if (reponse == "o") {
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
            // UPDATE Article (nom_article,prix_article,quant_article)
            Console.ReadLine();
        }

        public static void ShowAll() 
        {
            Console.Clear();

            Console.WriteLine("Afficher tous les articles.");
            // Affiche toutes les lignes de la liste
            /*for (int i = 0; i < Stock.Count; i++) {
                Console.WriteLine(Stock[i].ToString());
            }*/
            // SELECT * FROM Article

            // Affiche tout les articles de la liste sous forme de tableau.
            var table = new ConsoleTable("NUMÉRO", "NOM", "PRIX", "QUANTITÉ");
            for (int i = 0; i < Stock.Count; i++) {
                table.AddRow(Stock[i].Number, Stock[i].Name, Stock[i].Price, Stock[i].Quantity);
            }

            table.Write(Format.Alternative);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
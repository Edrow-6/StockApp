using System;
using Figgle;

namespace StockApp_Console.Utils
{
    class Menu
    {
        public static int Show(string menuName, params string[] options)
        {
            // Variables paramètrables
            const string version = "1.0";
            const int startX = 5;
            const int startY = 7;
            const int optionsPerLine = 1;
            const int spacingPerLine = 0;

            int currentSelection = 0;

            ConsoleKey key;
            Console.CursorVisible = false;
            Console.Clear();

            var exitMenu = false;

            // FONCTIONNEMENT UNIVERSEL POUR AVOIR AUTANT DE MENU QUE L'ON VEUT
            while (!exitMenu)
            {
                do
                {
                    Console.Title = $"GStock Console -  v{version}";
                    PageTitle($"GStock v{version}");

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                        if (i == currentSelection)
                        {
                            Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine - 3, startY + i / optionsPerLine);

                            if (menuName == "main" && currentSelection != 6 || menuName == "search" && currentSelection != 3)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("⮞ ");
                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                            else if (menuName == "search" && currentSelection == 3) 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("⮜ ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }
                            else if (menuName == "main" && currentSelection == 6)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("⮜ ");
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }
                        }

                        Console.Write(options[i]);

                        Console.ResetColor();
                    }

                    key = Console.ReadKey(true).Key;

                    // SELECTION DE LIGNES/MOTS POUR ATTRIBUTION COULEUR ET DE "currentSelection" EN RAPPORT DU NOMBRES D'OPTIONS PAR LIGNES
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (currentSelection >= optionsPerLine) currentSelection -= optionsPerLine;
                                break;
                            }
                        case ConsoleKey.DownArrow:
                            {
                                if (currentSelection + optionsPerLine < options.Length) currentSelection += optionsPerLine;
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                exitMenu = true;
                                break;
                            }
                    }
                } while (key != ConsoleKey.Enter);

                // DÉFINITION DES FONCTIONS PAR MENUS
                if (menuName == "main") // MENU PRINCIPAL
                {
                    switch (currentSelection)
                    {
                        case 0:
                            {
                                Program.SearchMenu();
                                break;
                            }
                        case 1:
                            {
                                Program.AddArticle();
                                break;
                            }
                        case 2:
                            {
                                Program.DeleteArticle();
                                break;
                            }
                        case 3:
                            {
                                Program.EditArticle();
                                break;
                            }
                        case 4:
                            {
                                Program.ShowAll();
                                break;
                            }
                        case 5:
                            {
                                Program.ShowSettings();
                                break;
                            }
                        case 6:
                            {
                                if (Confirm("Êtes-vous sûr de vouloir quitter ?"))
                                {
                                    exitMenu = true;
                                }
                                break;
                            }
                    }
                }
                else if (menuName == "search") // SOUS MENU DE RECHERCHER
                {
                    switch (currentSelection)
                    {
                        case 0:
                            {
                                Program.SearchByReference();
                                break;
                            }
                        case 1:
                            {
                                Program.SearchByName();
                                break;
                            }
                        case 2:
                            {
                                Program.SearchByPriceInterval();
                                break;
                            }
                        case 3:
                            {
                                exitMenu = true;
                                break;
                            }
                    }
                }
            }
            Console.CursorVisible = false;

            return currentSelection;
        }

        // FORMATAGE DES MESSAGES DE TOUT TYPES
        public static void DisplayMessage(string type, string message, bool next, string errorException = "")
        {
            switch (type)
            {
                case "error":
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errorException);

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ERREUR ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" " + message);
                        if (next)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(" (appuyez sur une touche pour continuer...)");
                        }
                        Console.ResetColor();
                        break;
                    }
                case "success":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" SUCCÈS ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(" " + message);
                        if (next)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(" (appuyez sur une touche pour continuer...)");
                        }
                        Console.ResetColor();
                        break;
                    }
                case "info":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" INFO ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine(" " + message);
                        if (next)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(" (appuyez sur une touche pour continuer...)");
                        }
                        Console.ResetColor();
                        break;
                    }
                case "warning":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ALERTE ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(" " + message);
                        if (next)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(" (appuyez sur une touche pour continuer...)");
                        }
                        Console.ResetColor();
                        break;
                    }
                case "unknown":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" MESSAGE ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(" " + message);
                        if (next)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(" (appuyez sur une touche pour continuer...)");
                        }
                        Console.ResetColor();
                        break;
                    }
            }
        }

        // FORMATAGE DES ENTRÉES DE L'UTILISATEUR (ex. Couleur)
        public static ReadOnlySpan<char> UserInput()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        // BOOLEAN DE CONFIRMATION D'ACTION
        public static bool Confirm(string title)
        {
            ConsoleKey response;
            do
            {
                Console.Write($"{title}");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" (O/N) ");
                Console.ResetColor();
                Console.Write("? ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter && response != ConsoleKey.O && response != ConsoleKey.N)
                {
                    DisplayMessage("error", "Veuillez saisir une réponse valide.", false);
                } else if (response == ConsoleKey.N)
                {
                    DisplayMessage("info", "Opération annulée...", false);
                }
            } 
            while (response != ConsoleKey.O && response != ConsoleKey.N);

            Console.WriteLine("\n");
            Console.ResetColor();

            return response == ConsoleKey.O;
        }

        // TITRE DE "PAGE" EN POLICE ASCII
        public static void PageTitle(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(FiggleFonts.Slant.Render("  " + title));
            Console.ResetColor();
        }
    }
}
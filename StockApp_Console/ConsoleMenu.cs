using System;
using Figgle;

namespace StockApp_Console
{
    class ConsoleMenu
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

            // Stockage de la clé
            ConsoleKey key;
            Console.CursorVisible = false;
            Console.Clear();

            var exitMenu = false;

            while (!exitMenu)
            {
                do
                {
                    Console.Title = $"Stock App | Version {version}";
                    PageTitle("StockApp");

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                        if (i == currentSelection)
                        {
                            Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine - 2, startY + i / optionsPerLine);

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

                if (menuName == "main")
                {
                    switch (currentSelection + 1)
                    {
                        case 1:
                            {
                                Program.SearchMenu();
                                break;
                            }
                        case 2:
                            {
                                Program.AddArticle();
                                break;
                            }
                        case 3:
                            {
                                Program.DeleteArticle();
                                break;
                            }
                        case 4:
                            {
                                Program.EditArticle();
                                break;
                            }
                        case 5:
                            {
                                Program.ShowAll();
                                break;
                            }
                        case 6:
                            {
                                Program.ShowSettings();
                                break;
                            }
                        case 7:
                            {
                                if (Confirm("Êtes-vous sûr de vouloir quitter ?"))
                                {
                                    exitMenu = true;
                                }
                                break;
                            }
                    }
                }
                else if (menuName == "search")
                {
                    switch (currentSelection + 1)
                    {
                        case 1:
                            {
                                Program.SearchByReference();
                                break;
                            }
                        case 2:
                            {
                                Program.SearchByName();
                                break;
                            }
                        case 3:
                            {
                                Program.SearchByPriceInterval();
                                break;
                            }
                        case 4:
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

        //REWRITE NEEDED public static ConsoleTable table = new ConsoleTable("NUMÉRO", "NOM", "PRIX", "QUANTITÉ");

         /*public static void DisplayTable(int stockNumber, string stockName, float stockPrice, int stockQuantity)
         {
             Table.AddRow(stockNumber, stockName, stockPrice, stockQuantity);
         }

        public static void DisplayTable(int stockNumber, string stockName, float stockPrice, int stockQuantity)
        {
            table.AddRow(stockNumber, stockName, stockPrice, stockQuantity);
            table.Write(Format.Alternative);

        }*/

        public static void DisplayMessage(string type, string message, string errorException = "")
        {
            switch (type)
            {
                case "error":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(errorException);

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" ERREUR ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" " + message);
                        Console.ResetColor();
                        break;
                    }
                case "success":
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" SUCCÈS ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(" " + message);
                        Console.ResetColor();
                        break;
                    }
                case "info":
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" INFO ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine(" " + message);
                        Console.ResetColor();
                        break;
                    }
                case "warning":
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" AVERTISSEMENT ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(" " + message);
                        Console.ResetColor();
                        break;
                    }
            }
        }

        public static ReadOnlySpan<char> UserInput()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

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
                if (response != ConsoleKey.Enter && response != ConsoleKey.O)
                {
                    DisplayMessage("error", "Veuillez saisir une réponse valide.");
                }
            } 
            while (response != ConsoleKey.O && response != ConsoleKey.N);

            Console.WriteLine("\n");
            Console.ResetColor();

            return response == ConsoleKey.O;
        }

        public static void PageTitle(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(FiggleFonts.Slant.Render("  " + title));
            Console.ResetColor();
        }
    }
}
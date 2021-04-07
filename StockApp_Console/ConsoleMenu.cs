using System;
using ConsoleTables;

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
                    Console.Clear();
                    Console.WriteLine(Figgle.FiggleFonts.Slant.Render("  Stock App"));

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
                            }else if (menuName == "search" && currentSelection == 3) {
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

        public static void DisplayError(string errorMessage, string errorException = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorException);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

        public static bool Confirm(string title)
        {
            ConsoleKey response;
            Console.WriteLine();
            do
            {
                Console.Write($"{title} [y/n] ");
                response = Console.ReadKey(false).Key;
                if (response != ConsoleKey.Enter && response != ConsoleKey.Y)
                {
                    DisplayError("Veuillez saisir une réponse valide.");
                }
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            return (response == ConsoleKey.Y);
        }
    }
}
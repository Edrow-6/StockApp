using System;
using ConsoleTables;

namespace StockApp
{
    class ConsoleMenu
    {
        public static int Show(string menuName, bool menuLoop, params string[] options)
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

            while (menuLoop)
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

                            if (menuName == "main" && currentSelection != 5 || menuName == "search" && currentSelection != 3)
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
                            else if (menuName == "main" && currentSelection == 5)
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
                                if (menuLoop == false) return -1;
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
                                menuLoop = false;
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
                                menuLoop = false;
                                break;
                            }
                    }
                }
                else if (menuName == "confirm")
                {

                }

            }
            Console.CursorVisible = false;

            return currentSelection;
        }

        /* public static ConsoleTable Table = new ConsoleTable("NUMÉRO", "NOM", "PRIX", "QUANTITÉ");

         public static void DisplayTable(int stockNumber, string stockName, float stockPrice, int stockQuantity)
         {
             Table.AddRow(stockNumber, stockName, stockPrice, stockQuantity);
         }*/

        public static void DisplayTable(int stockNumber, string stockName, float stockPrice, int stockQuantity)
        {
            var table = new ConsoleTable("NUMÉRO", "NOM", "PRIX", "QUANTITÉ");
            table.AddRow(stockNumber, stockName, stockPrice, stockQuantity);
            table.Write(Format.Alternative);
        }

        public static void DisplayError(string errorMessage, string errorException = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorException);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

        public static bool PromptConfirmation(string confirmText)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(confirmText + " [y/n] : ");
            ConsoleKey response = Console.ReadKey(false).Key;
            Console.WriteLine();
            Console.ResetColor();
            return (response == ConsoleKey.Y);
        }
    }
}
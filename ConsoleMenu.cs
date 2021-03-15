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
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("➤ ");
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
                    }
                }
                else if (menuName == "confirm")
                {

                }

            }
            Console.CursorVisible = false;

            return currentSelection;
        }

        public static void DisplayTable(int stockNumber, string stockName, float stockPrice, int stockQuantity)
        {
            var table = new ConsoleTable("NUMÉRO", "NOM", "PRIX", "QUANTITÉ");
            table.AddRow(stockNumber, stockName, stockPrice, stockQuantity);
            table.Write(Format.Alternative);
        }

        public static void DisplayError()
        {

        }
    }
}
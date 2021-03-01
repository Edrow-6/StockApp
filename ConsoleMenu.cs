using System;

namespace StockApp 
{
    class ConsoleMenu 
    {
        public static int MultipleChoice(bool menuLoop, params string[] options) 
        {
            // Variables paramètrables
            const int startX = 5;
            const int startY = 7;
            const int optionsPerLine = 1;
            const int spacingPerLine = 0;

            int currentSelection = 0;

            // Stockage de la clé
            ConsoleKey key;
            Console.CursorVisible = false;

            while (menuLoop) {

                do {
                    Console.Clear();
                    Console.WriteLine(Figgle.FiggleFonts.Slant.Render("  Stock App"));

                    for (int i = 0; i < options.Length; i++) {
                        Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                        if (i == currentSelection) {
                            Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine - 2, startY + i / optionsPerLine);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("➤ ");
                        }

                        Console.Write(options[i]);

                        Console.ResetColor();
                    }

                    key = Console.ReadKey(true).Key;

                    switch (key) {
                        case ConsoleKey.LeftArrow: {
                            if (currentSelection % optionsPerLine > 0) currentSelection--;
                            break;
                        }
                        case ConsoleKey.RightArrow: {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1) currentSelection++;
                            break;
                        }
                        case ConsoleKey.UpArrow: {
                            if (currentSelection >= optionsPerLine) currentSelection -= optionsPerLine;
                            break;
                        }
                        case ConsoleKey.DownArrow: {
                            if (currentSelection + optionsPerLine < options.Length) currentSelection += optionsPerLine;
                            break;
                        }
                        case ConsoleKey.Escape: {
                            if (menuLoop == false) return -1;
                            break;
                        }
                    }
                } while (key != ConsoleKey.Enter);

                switch (currentSelection + 1) {
                    case 1: {
                        Console.CursorVisible = true;
                        Program.Search();
                        break;
                    }
                    case 2: {
                        Console.CursorVisible = true;
                        Program.AddArticle();
                        break;
                    }
                    case 3: {
                        Console.CursorVisible = true;
                        Program.DeleteArticle();
                        break;
                    }
                    case 4: {
                        Console.CursorVisible = true;
                        Program.EditArticle();
                        break;
                    }
                    case 5: {
                        Console.CursorVisible = true;
                        Program.SearchByName();
                        break;
                    }
                    case 6: {
                        Console.CursorVisible = true;
                        Program.SearchBySellInterval();
                        break;
                    }
                    case 7: {
                        Console.CursorVisible = false;
                        Program.ShowAll();
                        break;
                    }
                    case 8: {
                        Console.CursorVisible = false;
                        menuLoop = false;
                        break;
                    }
                    default: {
                        menuLoop = true;
                        break;
                    }
                }
            }
            Console.CursorVisible = false;

            return currentSelection;
        }
    }
}
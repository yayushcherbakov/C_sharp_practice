using System;

namespace BullsAndCows
{
    partial class Program
    {
        static void Main()
        {
            // Флаг продолжения игры.
            bool isContinueGame = true;
            do
            {
                DisplayRulesToConsole();
                PlayNewGame();
                Console.WriteLine("Для выхода нажмите \"Escape\", для продолжения игры - любую другую клавишу.");
                isContinueGame = Console.ReadKey().Key != ConsoleKey.Escape;
                Console.Clear();
            } while (isContinueGame);
        }
    }
}

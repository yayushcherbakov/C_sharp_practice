using System;
using System.Linq;

namespace BullsAndCows
{
    partial class Program
    {
        /// <summary>
        /// Запускаем игру.
        /// </summary>
        private static void PlayNewGame()
        {
            int sequenceLength = GetSequenceLenght();
            int[] sequence = CreateRandomSequence(sequenceLength);
            do
            {
                int[] userSequence = GetUserSequence(sequenceLength);
                int countBulls = GetCountBulls(sequenceLength, sequence, userSequence);
                int countCows = GetCountCows(sequenceLength, sequence, userSequence, countBulls);
                DisplayBullsAndCows(countBulls, countCows);
                if (countBulls == sequenceLength)
                {
                    Console.Clear();
                    Console.WriteLine("Good Game Well Played:) Поздравляю c победой! Желаю успехов и удачи на экзамене!");
                    Console.WriteLine("Спасибо за внимание!");
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// Вывод на консоль количества "быков" и "коров".
        /// </summary>
        /// <param name="countBulls">Количество "быков".</param>
        /// <param name="countCows">Количество "коров".</param>
        private static void DisplayBullsAndCows(int countBulls, int countCows)
        {
            Console.WriteLine($"Количество \"коров\": {countCows}, количество \"быков\": {countBulls}.");
            Console.WriteLine();
        }

        /// <summary>
        /// Получает количество "коров" из двух последовательностей.
        /// </summary>
        /// <param name="sequenceLength">Длина последовательности.</param>
        /// <param name="sequence">Числовая последовательность в виде массива целочисленных непвоторяющихся элементов.</param>
        /// <param name="userSequence">Числоввя последовательность пользователя в виде масива целочисленных неповторяющихся элементов.</param>
        /// <param name="countBulls">Количество "быков".</param>
        /// <returns>Количество "коров".</returns>
        private static int GetCountCows(int sequenceLength, int[] sequence, int[] userSequence, int countBulls)
        {
            int countCoincidences = 0;
            for (int i = 0; i < sequenceLength; i++)
            {
                if (IsSequenceHasValue(sequenceLength, sequence, userSequence[i]))
                {
                    countCoincidences++;
                }
            }
            return countCoincidences - countBulls;
        }

        /// <summary>
        /// Проверяет наличие элемента с конкретным значением в массиве. 
        /// </summary>
        /// <param name="sequenceLength">Длина последовательности.</param>
        /// <param name="sequence">Числовая последовательность в виде массива целочисленных непвоторяющихся элементов.</param>
        /// <param name="value">Искомое значение.</param>
        /// <returns>Если элемент найден - true.</returns>
        private static bool IsSequenceHasValue(int sequenceLength, int[] sequence, int value)
        {
            for (int j = 0; j < sequenceLength; j++)
            {
                if (sequence[j] == value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получает количество "быков" из двух последовательностей.
        /// </summary>
        /// <param name="sequenceLength">Длина последовательности.</param>
        /// <param name="sequence">Числовая последовательность в виде массива целочисленных непвоторяющихся элементов.</param>
        /// <param name="userSequence">Числоввя последовательность пользователя в виде масива целочисленных неповторяющихся элементов.</param>
        /// <returns>Количество "быков".</returns>
        private static int GetCountBulls(int sequenceLength, int[] sequence, int[] userSequence)
        {
            int countBulls = 0;
            for (int i = 0; i < sequenceLength; i++)
            {
                if (userSequence[i] == sequence[i])
                {
                    countBulls++;
                }
            }
            return countBulls;
        }

        /// <summary>
        /// Получает корректно введенную одной строкой с консоли пользователем числовую последовательность.
        /// </summary>
        /// <param name="sequenceLength">Длина последовательности.</param>
        /// <returns>Числоввя последовательность пользователя в виде масива целочисленных неповторяющихся элементов.</returns>
        private static int[] GetUserSequence(int sequenceLength)
        {
            Console.WriteLine();
            Console.WriteLine("Введите предполагаемую последоательность цифр одной строкой! Помните, что она может начинаться с нуля и в ней не должно быть повторяющихся цифр.");
            string userInput = Console.ReadLine();
            while (!long.TryParse(userInput, out long userInputNumber) || userInput.Length != sequenceLength || userInput.ToCharArray().Distinct().Count() != sequenceLength)
            {
                Console.WriteLine("Ошибочка ввода! Попробуйте еще разок!");
                Console.WriteLine($"Последовательность должна состоять только из цифр. Они не должны повторяться. Её длина - {sequenceLength}.");
                userInput = Console.ReadLine();
            }
            int[] userSequence = new int[sequenceLength];
            for (int i = 0; i < sequenceLength; i++)
            {
                userSequence[i] = userInput[i] - '0';
            }
            return userSequence;
        }

        /// <summary>
        /// Генерирует случайную последовательность из неповторяющихся цифр.
        /// </summary>
        /// <param name="sequenceLength">Длина последовательности.</param>
        /// <returns>Числовая последовательность в виде массива целочисленных непвоторяющихся элементов.</returns>
        private static int[] CreateRandomSequence(int sequenceLength)
        {
            int[] sequence = new int[sequenceLength];
            for (int i = 0; i < sequenceLength; i++)
            {
                sequence[i] = -1;
            }
            Random rnd = new Random();
            for (int i = 0; i < sequenceLength; i++)
            {
                int temp = rnd.Next(0, 10);
                while (IsSequenceHasValue(sequenceLength, sequence, temp))
                {
                    temp = rnd.Next(0, 10);
                }
                sequence[i] = temp;
            }

            return sequence;
        }

        /// <summary>
        /// Получает длину последовательности.
        /// </summary>
        /// <returns>Длина последовательности.</returns>
        private static int GetSequenceLenght()
        {
            Console.WriteLine("Введите желаемую длину загадываемого числа от 1 до 10.");
            int sequenceLength;
            while (!int.TryParse(Console.ReadLine(), out sequenceLength) || sequenceLength < 1 || sequenceLength > 10)
            {
                Console.WriteLine("Ошибочка ввода! Попробуйте еще разок!");
            }
            return sequenceLength;
        }

        /// <summary>
        /// Выводит правила игры на консоль.
        /// </summary>
        public static void DisplayRulesToConsole()
        {
            Console.WriteLine("Игра \"Быки и коровы\"");
            Console.WriteLine("Правила игры:");
            Console.WriteLine("  1. Пользователь вводит длину угадываемой последовательности от 1 до 10.");
            Console.WriteLine("  2. Компьютер генерирует последовательность выбранной длины, состоящую из неповторяющихся цифр (возможен лидирующий ноль).");
            Console.WriteLine("  3. Затем пользователь пытается угадать заданную последовательность.");
            Console.WriteLine("  4. Компьютер выводит сообщение о том, сколько цифр (\"коров\") угадано, но не расположено на своих местах, и сколько цифр (\"быков\") угадано и находится на своих местах.");
            Console.WriteLine("  5. Раунды продолжаются до тех пор, пока пользователь не отгадает загаданное число.");
            Console.WriteLine("  6. Для более подробного ознакомления с правилами рекомендую перейти по ссылке: https://en.wikipedia.org/wiki/Bulls_and_Cows.");
            Console.WriteLine("Good Luck Have Fun:)");
        }
    }
}

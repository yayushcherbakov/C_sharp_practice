using System;
using System.IO;
using System.Text;

namespace MatrixCalculator
{
    class Program
    {
        private static readonly int _maxMatrixSize = 10;

        /// <summary>
        /// Точка входа в программу. 
        /// </summary>
        static void Main()
        {
            ShowHelp();
            try
            {
                do
                {
                    if (!ComandType.TryParse(Console.ReadLine(), out ComandType comandNumber))
                    {
                        Console.WriteLine("Нет команды с данным номером. Попробуйте еще раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case ComandType.GetMatrixTrace:
                            RunMatrixTraceCommand();
                            break;

                        case ComandType.MatrixTranspose:
                            RunMatrixTransposeCommand();
                            break;

                        case ComandType.MatrixSum:
                            RunMatrixSumCommand();
                            break;

                        case ComandType.MatrixSubtract:
                            RunMatrixSubtractCommand();
                            break;

                        case ComandType.MatrixMultiply:
                            RunMatrixMultiplyCommand();
                            break;

                        case ComandType.MatrixMultiplyToConst:
                            RunMatrixMultiplyToConstCommand();
                            break;

                        case ComandType.GetMatrixDeterminant:
                            RunGetMatrixDeterminantCommand();
                            break;

                        case ComandType.MatrixSolutionByCramer:
                            RunMatrixSolutionByCramerCommand();
                            break;

                        case ComandType.Exit:
                            return;

                        default:
                            Console.WriteLine("Нет команды с данным номером. Попробуйте еще раз.");
                            break;
                    }
                } while (true);
            }

            catch (Exception)
            {
                Console.WriteLine("Не удалось выполнить команду.");
            }
        }

        /// <summary>
        /// Запускает команду транспонирования матрицы.
        /// </summary>
        private static void RunMatrixTransposeCommand()
        {
            Console.Clear();

            var matrixSize = GetRectangleMatrixSize();
            var matrix = InputRectangleMatrix(matrixSize);

            Console.Clear();

            Console.WriteLine("Введенная матрица:");
            ShowMatrix(matrix);

            Console.WriteLine("Транспонированная матрица от введенной матрицы:");
            ShowMatrixTranspose(matrix);

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Запускает команду нахождения определителя матрицы.
        /// </summary>
        private static void RunGetMatrixDeterminantCommand()
        {
            Console.Clear();

            var matrixSize = GetSquareMatrixSize();

            uint[] matrixSizes = new uint[2] { matrixSize, matrixSize };
            var matrix = InputRectangleMatrix(matrixSizes);

            Console.Clear();

            Console.WriteLine("Матрица для нахождения детерминанта:");
            ShowMatrix(matrix);

            double det = GetMatrixDeterminant(matrixSize, matrix);
            Console.WriteLine($"Детерминант матрицы: {det}");

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Запускает команду решения СЛАУ методом Крамера.
        /// </summary>
        private static void RunMatrixSolutionByCramerCommand()
        {
            Console.Clear();

            uint[] matrixSize = GetSizeMatrixForCramer();
            var fullMatrix = InputRectangleMatrix(matrixSize);

            Console.Clear();

            Console.WriteLine("Введенная матрица:");
            ShowMatrix(fullMatrix);

            SeparateMatrixForCramer(matrixSize, fullMatrix, out double[,] matrix, out double[] freeMembers);

            double mainDet = GetMatrixDeterminant(matrixSize[0], matrix);
            if (mainDet == 0.0)
            {
                Console.WriteLine("Детерминант равен 0. Нельзя применять метод Крамера. Если определитель системы равен нулю, то система может быть как совместной, так и несовместной.");
                Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
                ShowHelp();
                return;
            }

            double[] solution = new double[matrixSize[0]];
            for (uint k = 0; k < matrixSize[0]; k++)
            {
                double[,] currentMatrix = GetCurrentMatrixForCramer(matrixSize, fullMatrix, freeMembers, k);
                solution[k] = GetMatrixDeterminant(matrixSize[0], currentMatrix) / mainDet;
            }

            Console.WriteLine("Решение системы:");
            for (uint k = 0; k < matrixSize[0]; k++)
            {
                Console.WriteLine($"X{k+1} = {solution[k]}");
            }

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Получет размер матрицы для метода Крамера.
        /// </summary>
        /// <returns>Размер матрицы для метода Крамера.</returns>
        private static uint[] GetSizeMatrixForCramer()
        {
            uint[] matrixSize = new uint[2];
            Console.WriteLine($"Введите число n - количество строк матрицы. Оно должно быть меньше {_maxMatrixSize + 1} и больше 0. ");
            Console.WriteLine("Чило столбцов будет n+1, так как добавляется столбец свободных членов системы.");
            while (!uint.TryParse(Console.ReadLine(), out matrixSize[0]) || matrixSize[0] > _maxMatrixSize || matrixSize[0] < 1)
            {
                Console.WriteLine("Некорректный порядок квадратной матрицы. Попробуйте еще раз!");
            }
            matrixSize[1] = matrixSize[0] + 1;
            return matrixSize;
        }

        /// <summary>
        /// Отделяет матрицу с неизвестными и столбец свободных членов от расширенной матрицы.
        /// </summary>
        /// <param name="matrixSize">Размер расширенной матрицы.</param>
        /// <param name="fullMatrix">Расширенная матрица.</param>
        /// <param name="matrix">Матрица с неизвестными.</param>
        /// <param name="freeMembers">Столбец свободных членов.</param>
        private static void SeparateMatrixForCramer(uint[] matrixSize, double[,] fullMatrix, out double[,] matrix, out double[] freeMembers)
        {
            matrix = new double[matrixSize[0], matrixSize[0]];
            for (uint i = 0; i < matrixSize[0]; i++)
            {
                for (uint j = 0; j < matrixSize[0]; j++)
                {
                    matrix[i, j] = fullMatrix[i, j];
                }
            }
            freeMembers = new double[matrixSize[0]];
            for (uint i = 0; i < matrixSize[0]; i++)
            {
                freeMembers[i] = fullMatrix[i, matrixSize[0]];
            }
        }

        /// <summary>
        /// Получает текущую матрицу для нахождения определителя.
        /// </summary>
        /// <param name="matrixSize">Размер расширенной матрицы.</param>
        /// <param name="fullMatrix">Расширенная матрица.</param>
        /// <param name="freeMembers">Столбец свободных членов.</param>
        /// <param name="k">Номер заменяемого столбца.</param>
        /// <returns> Текущая матрица для нахождения определителя.</returns>
        private static double[,] GetCurrentMatrixForCramer(uint[] matrixSize, double[,] fullMatrix, double[] freeMembers, uint k)
        {
            double[,] currentMatrix = new double[matrixSize[0], matrixSize[0]];
            for (uint i = 0; i < matrixSize[0]; i++)
            {
                for (uint j = 0; j < matrixSize[0]; j++)
                {
                    if (j != k)
                    {
                        currentMatrix[i, j] = fullMatrix[i, j];
                    }
                    else
                    {
                        currentMatrix[i, j] = freeMembers[i];
                    }
                }
            }

            return currentMatrix;
        }

        /// <summary>
        ///  Вычисляет детерминант квадртаной матрицы.
        /// </summary>
        /// <param name="matrixSize">Порядок матрицы.</param>
        /// <param name="matrix">Матрицы для вычисления детерминанта.</param>
        /// <returns>Детерминант.</returns>
        private static double GetMatrixDeterminant(uint matrixSize, double[,] matrix)
        {
            if (IsMatrixHasZeroRow(matrixSize, matrix) || IsMatrixHasZeroColumn(matrixSize, matrix))
            {
                return 0.0;
            }

            for (int i = 0; i < matrixSize; i++)
            {
                for (int k = i + 1; k < matrixSize; k++)
                {
                    if (matrix[i, i] == 0)
                    {
                        if (ReplaceZerosOnMainDiagonal(matrixSize, matrix, i))
                        {
                            return 0.0;
                        }
                    }

                    double temp = matrix[k, i] / matrix[i, i];
                    for (int j = i; j < matrixSize; j++)
                    {
                        matrix[k, j] = matrix[k, j] - matrix[i, j] * temp;
                    }
                }
            }

            double det = 1.0;
            for (int i = 0; i < matrixSize; i++)
            {
                det *= matrix[i, i];
            }

            return det;
        }

        /// <summary>
        /// Избавляется от нулей на галвной диагонали матрицы.
        /// </summary>
        /// <param name="matrixSize">Размеры матрицы.</param>
        /// <param name="matrix">Матрица.</param>
        /// <param name="startRow">Начальное значение строки, равное нулю.</param>
        private static bool ReplaceZerosOnMainDiagonal(uint matrixSize, double[,] matrix, int startRow = 0)
        {
            for (int i = startRow; i < matrixSize; i++)
            {
                if (matrix[i, i] == 0)
                {
                    for (int j = (int)matrixSize - 1; j >= startRow; j--)
                    {
                        if (matrix[j, i] != 0)
                        {
                            for (int k = 0; k < matrixSize; k++)
                            {
                                matrix[i, k] += matrix[j, k];

                            }
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < matrixSize - 1; i++)
            {
                if (matrix[i, i] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверяет матрицу на наличие строки из одних нулей.
        /// </summary>
        /// <param name="matrixSize">Размеры матрицы.</param>
        /// <param name="matrix">Матрица для проверки.</param>
        /// <returns>Результат проверки.</returns>
        private static bool IsMatrixHasZeroRow(uint matrixSize, double[,] matrix)
        {
            for (int i = 0; i < matrixSize; i++)
            {
                bool hasOnlyZero = true;
                for (int j = 0; j < matrixSize; j++)
                {
                    if (matrix[i, j] != 0.0)
                    {
                        hasOnlyZero = false;
                        break;
                    }
                }
                if (hasOnlyZero)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверяет матрицу на наличие столбца из одних нулей.
        /// </summary>
        /// <param name="matrixSize">Размеры матрицы.</param>
        /// <param name="matrix">Матрица.</param>
        /// <returns>Результат проверки.</returns>
        private static bool IsMatrixHasZeroColumn(uint matrixSize, double[,] matrix)
        {
            for (int i = 0; i < matrixSize; i++)
            {
                bool hasOnlyZero = true;
                for (int j = 0; j < matrixSize; j++)
                {
                    if (matrix[j, i] != 0.0)
                    {
                        hasOnlyZero = false;
                        break;
                    }
                }
                if (hasOnlyZero)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Запускает команду произведения двух матриц.
        /// </summary>
        private static void RunMatrixMultiplyCommand()
        {
            Console.Clear();

            Console.WriteLine("Матрицы можно умножать, если количество столбцов первой матрицы равно количеству строк второй матрицы.");

            var rowCountFirstMatrix = GetRowCountFirstMatrix();
            var rowCountSecondMatrix = GetRowCountSecondMatrix();
            var columnCountFirstMatrix = rowCountSecondMatrix;
            var columnCountSecondMatrix = GetColumnCountSecondMatrix();

            Console.WriteLine("Ввод первой матрицы:");
            uint[] firsMatrixSize = new uint[2] { rowCountFirstMatrix, columnCountFirstMatrix };
            var firstMatrix = InputRectangleMatrix(firsMatrixSize);

            Console.WriteLine("Ввод второй матрицы:");
            uint[] secondMatrixSize = new uint[2] { rowCountSecondMatrix, columnCountSecondMatrix };
            var secondMatrix = InputRectangleMatrix(secondMatrixSize);

            double[,] result = MultiplyMatrix(rowCountFirstMatrix, columnCountFirstMatrix, columnCountSecondMatrix, firstMatrix, secondMatrix);

            Console.Clear();

            Console.WriteLine("Первая матрица:");
            ShowMatrix(firstMatrix);

            Console.WriteLine("Вторая матрица:");
            ShowMatrix(secondMatrix);

            Console.WriteLine("Произведение двух введенных матриц:");
            ShowMatrix(result);

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Умножение двух матриц
        /// </summary>
        /// <param name="rowCountFirstMatrix">Количество строк первой матрицы.</param>
        /// <param name="columnCountFirstMatrix">Количество столбцов первой матрицы.</param>
        /// <param name="columnCountSecondMatrix">Количество столбцов второй матрицы.</param>
        /// <param name="firstMatrix">Первая матрица.</param>
        /// <param name="SecondMatrix">Вторая матрица.</param>
        /// <returns>Произведение двух матриц, записанное в первой матрице.</returns>
        private static double[,] MultiplyMatrix(uint rowCountFirstMatrix, uint columnCountFirstMatrix, uint columnCountSecondMatrix, double[,] firstMatrix, double[,] SecondMatrix)
        {
            double[,] result = new double[rowCountFirstMatrix, columnCountSecondMatrix];
            for (int l = 0; l < rowCountFirstMatrix; l++)
            {
                for (int n = 0; n < columnCountSecondMatrix; n++)
                {
                    double sum = 0.0;
                    for (int m = 0; m < columnCountFirstMatrix; m++)
                    {
                        sum += firstMatrix[l, m] * SecondMatrix[m, n];
                    }
                    result[l, n] = sum;
                }
            }

            return result;
        }

        /// <summary>
        /// Запускает команду разности двух матриц.
        /// </summary>
        private static void RunMatrixSubtractCommand()
        {
            Console.Clear();

            Console.WriteLine("Матрицы можно вычитать, если они одинакового размера.");
            Console.WriteLine("Вводимые данные о количестве строк и столбцов будут относиться к двум матрицам сразу.");

            var matrixSize = GetRectangleMatrixSize();

            Console.WriteLine("Ввод первой матрицы (из неё будет вычитаться вторая):");
            var firstMatrix = InputRectangleMatrix(matrixSize);

            Console.WriteLine("Ввод второй матрицы (она будет вычитаться из первой):");
            var secondMatrix = InputRectangleMatrix(matrixSize);

            Console.Clear();

            Console.WriteLine("Первая матрица:");
            ShowMatrix(firstMatrix);

            Console.WriteLine("Вторая матрица:");
            ShowMatrix(secondMatrix);

            Console.WriteLine("Матрица разности двух введенных матриц:");
            ShowMatrixSubtract(firstMatrix, secondMatrix);

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Запускает команду суммирования двух матриц.
        /// </summary>
        private static void RunMatrixSumCommand()
        {
            Console.Clear();

            Console.WriteLine("Матрицы можно складывать, если они одинакового размера.");
            Console.WriteLine("Вводимые данные о количестве строк и столбцов будут относиться к двум суммируемым матрицам сразу.");

            var matrixSize = GetRectangleMatrixSize();

            Console.WriteLine("Ввод первой матрицы:");
            var firstMatrix = InputRectangleMatrix(matrixSize);

            Console.WriteLine("Ввод второй матрицы:");
            var secondMatrix = InputRectangleMatrix(matrixSize);

            Console.Clear();

            Console.WriteLine("Первая матрица:");
            ShowMatrix(firstMatrix);

            Console.WriteLine("Вторая матрица:");
            ShowMatrix(secondMatrix);

            Console.WriteLine("Матрица суммы двух введенных матриц:");
            ShowMatrixSum(firstMatrix, secondMatrix);

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }


        /// <summary>
        /// Запускает команду умножения матрицы на число.
        /// </summary>
        private static void RunMatrixMultiplyToConstCommand()
        {
            Console.Clear();

            var matrixSize = GetRectangleMatrixSize();

            var matrix = InputRectangleMatrix(matrixSize);

            var multiplicationConst = GetMultiplicationConst();

            Console.Clear();

            Console.WriteLine("Введенная матрица:");
            ShowMatrix(matrix);

            Console.WriteLine($"Введенное число для умножения на матрицу: {multiplicationConst}");
            Console.WriteLine($"Матрица, умноженная на {multiplicationConst}:");

            ShowMatrixMultiplyToConst(matrix, multiplicationConst);

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Получает от пользователя число для умножения на матрицу.
        /// </summary>
        /// <returns>Число для умножения на матрицу.</returns>
        private static double GetMultiplicationConst()
        {
            Console.WriteLine("Введите число, на которое хотите умножить матрицу. Помните, что оно не может быть 0!");
            double multiplicationConst;
            while (!double.TryParse(Console.ReadLine(), out multiplicationConst) || multiplicationConst == 0)
            {
                Console.WriteLine("Некорректный ввод числа для умножения. Попробуйте еще раз!");
            }

            return multiplicationConst;
        }

        /// <summary>
        /// Запускает команду нахождения следа матрицы.
        /// </summary>
        private static void RunMatrixTraceCommand()
        {
            Console.Clear();

            var matrixSize = GetSquareMatrixSize();
            uint[] matrixSizes = new uint[2] { matrixSize, matrixSize };

            var matrix = InputRectangleMatrix(matrixSizes);
            double trace = GetTrace(matrix);

            Console.Clear();

            Console.WriteLine("Введенная матрица:");
            ShowMatrix(matrix);

            Console.WriteLine($"След матрицы - {trace}");

            Console.WriteLine($"Для завершения работы нажмите {(uint)ComandType.Exit}, для продолжения работы выберите номер команды.");
            ShowHelp();
        }

        /// <summary>
        /// Выводит разность матриц на консоль.
        /// </summary>
        /// <param name="firstMatrix">Первая матрица, из которой вычитается вторая.</param>
        /// <param name="secondMatrix">Вторая матрица, которая вычитается из первой.</param>
        private static void ShowMatrixSubtract(double[,] firstMatrix, double[,] secondMatrix)
        {
            var columnCount = firstMatrix.GetLength(1);
            var rowCount = firstMatrix.GetLength(0);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    firstMatrix[i, j] = firstMatrix[i, j] - secondMatrix[i, j];
                    Console.Write(firstMatrix[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Выводит на консоль сумму двух матриц.
        /// </summary>
        /// <param name="firstMatrix">Первая матрица для суммирования.</param>
        /// <param name="secondMatrix">Вторая матрица для суммирования.</param>
        private static void ShowMatrixSum(double[,] firstMatrix, double[,] secondMatrix)
        {
            var columnCount = firstMatrix.GetLength(1);
            var rowCount = firstMatrix.GetLength(0);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    firstMatrix[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
                    Console.Write(firstMatrix[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Получает след квадратной матрицы.
        /// </summary>
        /// <param name="matrix">Квадратная матрица.</param>
        /// <returns>След квадратной матрицы.</returns>
        private static double GetTrace(double[,] matrix)
        {
            var columnCount = matrix.GetLength(1);
            var rowCount = matrix.GetLength(0);

            double trace = 0.0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (i == j)
                    {
                        trace += matrix[i, j];
                    }
                }
            }

            return trace;
        }

        /// <summary>
        /// Ввод квадратной матрицы.
        /// </summary>
        /// <param name="matrixSize">Массив, содержащий количество строк и столбцов матрицы.</param>
        /// <returns>Введенную матрицу.</returns>
        private static double[,] InputRectangleMatrix(uint[] matrixSize)
        {
            ChooseInputMethod();

            do
            {
                if (!InputType.TryParse(Console.ReadLine(), out InputType comandNumber))
                {
                    Console.WriteLine("Нет метода ввода с данным номером. Попробуйте ввести еще раз.");
                    continue;
                }
                Console.WriteLine(comandNumber);
                switch (comandNumber)
                {
                    case InputType.Console:
                        return InputRectangleMatrixFromConsole(matrixSize);

                    case InputType.Random:
                        return InputRectangleMatrixRandom(matrixSize);

                    case InputType.File:
                        return InputRectangleMatrixFromFile(matrixSize);

                    default:
                        Console.WriteLine("Нет метода ввода с данным номером.");
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Выбор способа ввода.
        /// </summary>
        private static void ChooseInputMethod()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Выберите метод ввода данной матрицы:");
            Console.WriteLine("Выберите [1] для ввода матрицы при помощи считывания с консоли.");
            Console.WriteLine("Выберите [2] для ввода матрицы при помощи псевдослучайной генерации.");
            Console.WriteLine("Выберите [3] для ввода матрицы при попомщи считывания из файла.");
            Console.ResetColor();
        }

        /// <summary>
        /// Ввод с консоли от пользователя прямоугольной матрицы.
        /// </summary>
        /// <param name="matrixSize">Массив, содержащий количество строк и столбцов матрицы.</param>
        /// <returns>Введенная матрица.</returns>
        private static double[,] InputRectangleMatrixFromConsole(uint[] matrixSize)
        {
            double[,] matrix = new double[matrixSize[0], matrixSize[1]];

            Console.WriteLine("Введите матрицу по строкам! Каждый элемент в строке через пробел.");
            for (int i = 0; i < matrixSize[0]; i++)
            {
                string currentRow = Console.ReadLine();
                string[] currentRowElements = currentRow.Split();

                if (currentRowElements.Length != matrixSize[1])
                {
                    Console.WriteLine("Некорректное количество элементов в строке. Введите строку еще раз!");
                    i--;
                    continue;
                }
                for (int j = 0; j < matrixSize[1]; j++)
                {
                    if (!double.TryParse(currentRowElements[j], out matrix[i, j]))
                    {
                        Console.WriteLine("Один из элементов строки введен некорректно. Введите строку еще раз!");
                        i--;
                        break;
                    }
                }
            }
            return matrix;
        }

        /// <summary>
        /// Вводит псевдослучайно сгенерированную прямоугольную матрицу.
        /// </summary>
        /// <param name="matrixSize">Массив, содержащий количество строк и столбцов матрицы.</param>
        /// <returns>Матрица.</returns>
        private static double[,] InputRectangleMatrixRandom(uint[] matrixSize)
        {
            double[,] matrix = new double[matrixSize[0], matrixSize[1]];

            int seed = GetSeedForRandomGeneration();
            Random rand = new Random(seed);
            int[] range = GetRangeForRandomGeneration();

            for (int i = 0; i < matrixSize[0]; i++)
            {
                for (int j = 0; j < matrixSize[1]; j++)
                {
                    matrix[i, j] = rand.Next(range[0], range[1]);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Вводит прямоугольную матрицу из файла. 
        /// </summary>
        /// <param name="matrixSize">Массив, содержащий количество строк и столбцов матрицы.</param>
        /// <returns>Считанная из файла матрица.</returns>
        private static double[,] InputRectangleMatrixFromFile(uint[] matrixSize)
        {
            double[,] matrix = new double[matrixSize[0], matrixSize[1]];
            Console.WriteLine("Требования к файлу: " +
                "Все элементы матрицы должны находиться в диапазоне типа double. " +
                "Каждая строка матрицы соответствует строке в файле. " +
                "Значения в строке разделяются пробелом. " +
                "Не должно быть пустых строк.");
            Console.WriteLine("Введите полный путь к файлу.");
            do
            {
                try
                {
                    string userInput = Console.ReadLine();
                    var filePath = Path.Combine(userInput);

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"Файл с названием {filePath} не был найден. Попробуйте еще раз.");
                        continue;
                    }

                    if (!TryParseDataFromFile(matrixSize, matrix, filePath))
                    {
                        continue;
                    };

                    return matrix;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Недостаточно прав для чтения файла в данной директории. Попробуйте выбрать файл из другой директории.");
                    continue;
                }

            } while (true);
        }

        /// <summary>
        /// Позволяет определить кодировку файла по метке порядка байтов.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Возвращает кодировку файла. Кодировкой по умолчанию считается ASCII.</returns>
        public static Encoding GetEncoding(string filename)
        {
            // Считываем метку порядка байтов(BOM).
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Анализируем BOM.
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                return Encoding.UTF8;
            }
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0)
            {
                return Encoding.UTF32;
            }
            if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                return Encoding.Unicode;
            }
            if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                return Encoding.BigEndianUnicode;
            }
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
            {
                return new UTF32Encoding(true, true);
            }

            // Кодировкой по умолчанию считаем ASCII, как одну из самых распространенных.
            return Encoding.ASCII;
        }

        /// <summary>
        /// Проверяем на коррекность данные из файла.
        /// </summary>
        /// <param name="matrixSize">Размеры матрицы.</param>
        /// <param name="matrix">Матрица.</param>
        /// <param name="filePath">Полный путь к файлу.</param>
        /// <returns></returns>
        private static bool TryParseDataFromFile(uint[] matrixSize, double[,] matrix, string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath, GetEncoding(filePath));
            if (fileLines.Length != matrixSize[0])
            {
                Console.WriteLine("Количество строк в файле не совпадает с количеством строк в матрице. Попробуйте отредактировать файл и попробовать загрузить еще раз.");
                return false;
            }

            for (uint i = 0; i < matrixSize[1]; i++)
            {
                string currentRow = fileLines[i];
                string[] currentRowElements = currentRow.Split();
                if (currentRowElements.Length != matrixSize[1])
                {
                    Console.WriteLine($"Некорректное количество элементов в строке {i + 1}. Попробуйте отредактировать файл и попробовать загрузить еще раз.");
                    return false;
                }
                for (int j = 0; j < matrixSize[1]; j++)
                {
                    if (!double.TryParse(currentRowElements[j], out matrix[i, j]))
                    {
                        Console.WriteLine($"{j + 1} элемент в строке {i + 1} введен некорректно. Попробуйте отредактировать файл и попробовать загрузить еще раз.");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Получает от пользователя начальное значение для генерации псевдослучайных чисел.
        /// </summary>
        /// <returns>Начальное значение для генерации.</returns>
        private static int GetSeedForRandomGeneration()
        {
            Console.WriteLine("Введите начальное значение для генерации псевдослучайных элементов матрицы. Начальное значение - целое число.");
            int seed;
            while (!int.TryParse(Console.ReadLine(), out seed))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }
            return seed;
        }



        /// <summary>
        /// Получает диапазон от пользователя для генерации псевдослучайных чисел.
        /// </summary>
        /// <returns>Массив из 2 целых чисел, содержащий границы диапазона для генерации.</returns>
        private static int[] GetRangeForRandomGeneration()
        {
            int[] range = new int[2];
            Console.WriteLine($"Введите целое число n - включенный нижний предел возвращаемого случайного числа.");
            while (!int.TryParse(Console.ReadLine(), out range[0]))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }
            Console.WriteLine($"Введите целое число m - исключенный верхний предел возвращаемого случайного числа. Значение m должно быть больше или равно значению n.");
            while (!int.TryParse(Console.ReadLine(), out range[1]) || range[1] < range[0])
            {
                Console.WriteLine("Некорректный порядок квадратной матрицы. Попробуйте еще раз!");
            }
            return range;
        }

        /// <summary>
        /// Выводит на консоль умноженную на число матрицу.
        /// </summary>
        /// <param name="matrix">Матрица для умножения.</param>
        /// <param name="multiplicationConst">Число для умножения.</param>
        private static void ShowMatrixMultiplyToConst(double[,] matrix, double multiplicationConst)
        {
            var columnCount = matrix.GetLength(1);
            var rowCount = matrix.GetLength(0);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Console.Write(matrix[i, j] * multiplicationConst + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Выводит на консоль транспонированную матрицу.
        /// </summary>
        /// <param name="matrix">Матрица для транспонирования.</param>
        private static void ShowMatrixTranspose(double[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);

            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    Console.Write(matrix[j, i] + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Выводит матрицу на консоль.
        /// </summary>
        /// <param name="matrix">Матрица для вывода на консоль.</param>
        private static void ShowMatrix(double[,] matrix)
        {
            var columnCount = matrix.GetLength(1);
            var rowCount = matrix.GetLength(0);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Получает размер прямоугольной матрицы от пользователя. 
        /// </summary>
        /// <returns>Массив из двух положительных целых элементов, содержащий количество столбцов и строк прямоугольной матрицы.</returns>
        private static uint[] GetRectangleMatrixSize()
        {
            uint[] matrixSize = new uint[2];

            Console.WriteLine($"Введите число n - количество строк матрицы. Оно должно быть меньше {_maxMatrixSize + 1} и больше 0.");

            while (!uint.TryParse(Console.ReadLine(), out matrixSize[0]) || matrixSize[0] > _maxMatrixSize || matrixSize[0] < 1)
            {
                Console.WriteLine("Некорректный порядок квадратной матрицы. Попробуйте еще раз!");
            }

            Console.WriteLine($"Введите число m - количество столбцов матрицы. Оно должно быть меньше {_maxMatrixSize + 1} и больше 0.");

            while (!uint.TryParse(Console.ReadLine(), out matrixSize[1]) || matrixSize[1] > _maxMatrixSize || matrixSize[1] < 1)
            {
                Console.WriteLine("Некорректный порядок квадратной матрицы. Попробуйте еще раз!");
            }

            return matrixSize;
        }



        /// <summary>
        /// Получает размер квадратной матрицы от пользователя. 
        /// </summary>
        /// <returns>Размер квадратной матрицы.</returns>
        private static uint GetSquareMatrixSize()
        {
            Console.WriteLine($"Введите число n - порядок квадратной матрицы. Он должен быть меньше {_maxMatrixSize + 1} и больше 0.");

            uint matrixSize;

            while (!uint.TryParse(Console.ReadLine(), out matrixSize) || matrixSize > _maxMatrixSize || matrixSize < 1)
            {
                Console.WriteLine("Некорректный порядок квадратной матрицы. Попробуйте еще раз!");
            }

            return matrixSize;
        }



        /// <summary>
        /// Получает от пользователя количество строк второй матрицы для умножения матриц.
        /// </summary>
        /// <returns>Количество строк второй матрицы.</returns>
        private static uint GetRowCountSecondMatrix()
        {
            Console.WriteLine($"Введите число m - количество столбцов первой матрицы и строк второй матрицы. Оно должен быть не больше {_maxMatrixSize} и больше 0.");

            uint rowCountSecondMatrix;

            while (!uint.TryParse(Console.ReadLine(), out rowCountSecondMatrix) || rowCountSecondMatrix > _maxMatrixSize || rowCountSecondMatrix < 1)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }

            return rowCountSecondMatrix;
        }

        /// <summary>
        /// Получает от пользователя количество строк первой матрицы для умножения матриц.
        /// </summary>
        /// <returns>Количество строк первой матрицы.</returns>
        private static uint GetRowCountFirstMatrix()
        {
            Console.WriteLine($"Введите число l - количество строк первой матрицы. Оно должен быть не больше {_maxMatrixSize} и больше 0.");

            uint rowCountFirstMatrix;

            while (!uint.TryParse(Console.ReadLine(), out rowCountFirstMatrix) || rowCountFirstMatrix > _maxMatrixSize || rowCountFirstMatrix < 1)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }

            return rowCountFirstMatrix;
        }

        /// <summary>
        /// Получает от пользователя количество столбцов второй матрицы для умножения матриц.
        /// </summary>
        /// <returns>Количество столбцов второй матрицы.</returns>
        private static uint GetColumnCountSecondMatrix()
        {
            Console.WriteLine($"Введите число n - количество столбцов второй матрицы. Оно должено быть не больше {_maxMatrixSize} и больше 0.");

            uint columnCountSecondtMatrix;

            while (!uint.TryParse(Console.ReadLine(), out columnCountSecondtMatrix) || columnCountSecondtMatrix > _maxMatrixSize || columnCountSecondtMatrix < 1)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }

            return columnCountSecondtMatrix;
        }

        /// <summary>
        /// Выводит информацию о командах для пользователя.
        /// </summary>
        private static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Номера команд:");
            Console.WriteLine("Выберите [1] для нахождения следа матрицы.");
            Console.WriteLine("Выберите [2] для транспонирования матрицы.");
            Console.WriteLine("Выберите [3] для нахождения суммы двух матриц.");
            Console.WriteLine("Выберите [4] для нахождения разности двух матриц.");
            Console.WriteLine("Выберите [5] для нахождения произведения двух матриц.");
            Console.WriteLine("Выберите [6] для нахождения умножения матрицы на число.");
            Console.WriteLine("Выберите [7] для нахождения определителя квадратной матрицы.");
            Console.WriteLine("Выберите [8] для нахождения решения системы линейных уравнений методом Крамера.");
            Console.ResetColor();
        }
    }

    /// <summary>
    ///  Перечисление для типа команд.
    /// </summary>
    public enum ComandType : uint
    {
        Exit = 0,
        GetMatrixTrace = 1,
        MatrixTranspose = 2,
        MatrixSum = 3,
        MatrixSubtract = 4,
        MatrixMultiply = 5,
        MatrixMultiplyToConst = 6,
        GetMatrixDeterminant = 7,
        MatrixSolutionByCramer = 8
    }

    /// <summary>
    /// Перечисление для способов ввода пользователя.
    /// </summary>
    public enum InputType : uint
    {
        Console = 1,
        Random = 2,
        File = 3
    }
}

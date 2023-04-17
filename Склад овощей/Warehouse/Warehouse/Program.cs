using System;
using System.IO;
using System.Text;

namespace Warehouse
{

    class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Здравствуйте! Вас приветствует разработчик программы \"Овощной склад\".");
            Console.ResetColor();
            var inputType = GetInputType();
            Console.Clear();
            uint maxContainerCount = 10;
            decimal containerStorageFee = 10.0m;
            if (inputType == InputType.Console)
            {
                GetWareHouseParametersFromConsole(out maxContainerCount, out containerStorageFee);
            }
            if (inputType == InputType.File)
            {
                GetWareHouseParametersFromFile(out maxContainerCount, out containerStorageFee);
            }

            Warehouse warehouse = Warehouse.GetInstance(maxContainerCount, containerStorageFee);

            if (inputType == InputType.Console)
            {
                RequestAndExecuteComandsFromConsole(warehouse);
            }
            if (inputType == InputType.File)
            {
                RequestAndExecuteComandsFromFile(warehouse);
                Console.WriteLine(warehouse);
                Console.WriteLine("Команды из файла выполнены. Спасибо за внимание!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Получить и выполнить каманду из файла.
        /// </summary>
        /// <param name="warehouse">Склад.</param>
        private static void RequestAndExecuteComandsFromFile(Warehouse warehouse)
        {
            ShowFilesInfo();

            Console.WriteLine("Введите полный путь к файлу команд:");
            string[] comandLines = GetDataFromUserByFile();
            Console.WriteLine("Введите полный путь к файлу контейнеров:");
            string[] containerLines = GetDataFromUserByFile();
            uint currentConteinerIndex = 0;
            if (comandLines.Length < 1)
            {
                Console.WriteLine("Файл команд пуст.");
                return;
            }

            for (int i = 0; i < comandLines.Length; i++)
            {
                try
                {
                    if (comandLines[i] == "addContainer")
                    {
                        if (currentConteinerIndex >= containerLines.Length)
                        {
                            Console.WriteLine("В файле контейнеров больше нет контейнеров для добавления.");
                            continue;
                        }
                        AddContainerToWarehouseFromString(containerLines[currentConteinerIndex], currentConteinerIndex, warehouse);
                        continue;
                    }
                    if (comandLines[i].StartsWith("removeContainer "))
                    {
                        warehouse.RemoveContainerById(comandLines[i].Substring("removeContainer ".Length));
                        continue;
                    }
                    Console.WriteLine($"Некорректная команда в строке {i}.");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Некорректная команда в строке {i}.");
                }                
            }
        }

        /// <summary>
        /// Пробует добавить контейнер на склад со строки.
        /// </summary>
        /// <param name="containerString">Строка с параметрами контейнера.</param>
        /// <param name="line">Вводимая строка.</param>
        /// <param name="warehouse">Склад.</param>
        private static void AddContainerToWarehouseFromString(string containerString, uint line, Warehouse warehouse)
        {
            string[] parameters = containerString.Split();
            if (parameters.Length < 1 || parameters.Length % 2 == 0)
            {
                Console.WriteLine($"Некорректное количество параметров контейнера в строке {line}");
                return;
            }

            Container newContainer = new Container(parameters[0]);
            for (int i = 1; i + 1 < parameters.Length; i += 2)
            {
                uint weight;
                while (!uint.TryParse(parameters[i], out weight) || weight < 0)
                {
                    Console.WriteLine($"Некорректный вес ящика в строке {line}.");
                    return;
                }

                decimal pricePerKilogram;
                while (!decimal.TryParse(parameters[i + 1], out pricePerKilogram) || pricePerKilogram < 0.0m)
                {
                    Console.WriteLine($"Некорректная цена за килограмм ящика в строке {line}.");
                    return;
                }

                VegetableBox newVegetableBox = new VegetableBox(weight, pricePerKilogram);
                newContainer.AddBox(newVegetableBox);
            }
        }

        /// <summary>
        /// Получить данные пользователя из файла.
        /// </summary>
        /// <returns></returns>
        private static string[] GetDataFromUserByFile()
        {
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

                    if (!CheckFileSize(filePath))
                    {
                        Console.WriteLine($"Файл слишком большой. Попробуйте файл поменьше.");
                        continue;
                    }
                    return File.ReadAllLines(filePath);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Недостаточно прав для чтения файла в данной директории. Попробуйте выбрать файл из другой директории.");
                    continue;
                }

            } while (true);
        }

        /// <summary>
        /// Вывоит на консоль справку про файлы.
        /// </summary>
        private static void ShowFilesInfo()
        {
            Console.WriteLine("Файл описания действий - список из добавлений и удалений контейнеров со склада");
            Console.WriteLine();

            Console.WriteLine("Предусмотрены 2 команды:");
            Console.WriteLine();

            Console.WriteLine("addContainer - добавляет следующий контейнер из файла описания контейнера.");
            Console.WriteLine("Контейнеры должны быть упорядочены в порядке создания, так как они будут браться один за одним в порядке определенном в файле.");
            Console.WriteLine();

            Console.WriteLine("removeContainer - удаляет контейнер со склада по идентификатору. Принемает один параметр - идентификатор контейнера.");
            Console.WriteLine("Пример использования:");
            Console.WriteLine("removeContainer someId");
            Console.WriteLine();

            Console.WriteLine("Файл описание контейнера - вся информация о контейнере(включая список ящиков с овощами) в одной строке.");
            Console.WriteLine("Rонтейнеры упорядочены в порядке создания. Параметры контейнера перечисляются через пробел.");
            Console.WriteLine("Шаблон строки в файле контейнеров:");
            Console.WriteLine("{идентификатор контейнера} " +
                "{вес первого ящика} {цена за килограм веса первого ящика} " +
                "{вес второго ящика} {цена за килограм веса второго ящика} " +
                "... " +
                "{вес n-го ящика} {цена за килограм веса n-го ящика}");
            Console.WriteLine("Пример строки в файле контейнера:");
            Console.WriteLine("containerId 11 222 33 444 ");
            Console.WriteLine();
        }

        /// <summary>
        /// Получает параметры склада из файла.
        /// </summary>
        /// <param name="maxContainerCount">Вместительность склада.</param>
        /// <param name="containerStorageFee">Склад.</param>
        private static void GetWareHouseParametersFromFile(out uint maxContainerCount, out decimal containerStorageFee)
        {
            Console.WriteLine("Введите полный путь к файлу с описанием склада.");
            Console.WriteLine("В файле должно быть указано два значения с новой строки.");
            Console.WriteLine("В первой строке указывается вместительность склада.");
            Console.WriteLine("Во второй строке указывается стоимость хранения контейнера на складе.");
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

                    if (!CheckFileSize(filePath))
                    {
                        Console.WriteLine($"Файл слишком большой. Попробуйте файл поменьше.");
                        continue;
                    }

                    if (!TryParseWarehouseDataFromFile(filePath, out maxContainerCount, out containerStorageFee))
                    {
                        continue;
                    };
                    return;
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
        /// Проверяет вводимые данные на коррекность.
        /// </summary>
        /// <param name="filePath">Путь файла.</param>
        /// <param name="maxContainerCount">Максимальная вместителльность контейнера.</param>
        /// <param name="containerStorageFee">Стоимость содержания контейнера.</param>
        /// <returns></returns>
        private static bool TryParseWarehouseDataFromFile(string filePath, out uint maxContainerCount, out decimal containerStorageFee)
        {
            maxContainerCount = 0;
            containerStorageFee = 0m;
            string[] fileLines = File.ReadAllLines(filePath, GetEncoding(filePath));
            if (fileLines.Length != 2)
            {
                Console.WriteLine("Некоректное количество строк в файле. Отредактируйте файл и попробуйде еще раз.");
                return false;
            }
            if (!uint.TryParse(fileLines[0], out maxContainerCount))
            {
                Console.WriteLine("Некоректное значение вместительности склада. Требуется целое число больше нуля. Отредактируйте файл и попробуйде еще раз.");
                return false;
            }
            if (!decimal.TryParse(fileLines[1], out containerStorageFee))
            {
                Console.WriteLine("Некоректное значение стоимости хранения контейнера на складе. Требуется дробное число больше нуля. Отредактируйте файл и попробуйде еще раз.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка на величину файла. Файл не должен превышать 5MB.
        /// </summary>
        /// <param name="filePath"> Полный путь к файлу.</param>
        /// <returns>Возвращает true если файл меньше либо равен 5MB.</returns>
        private static bool CheckFileSize(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length <= 5_242_880;
        }

        /// <summary>
        /// Получает параметры склада с консоли.
        /// </summary>
        /// <param name="maxContainerCount">Максимальное количество контейнеров на складе.</param>
        /// <param name="containerStorageFee">Стоиость содержания контейнера на складе.</param>
        private static void GetWareHouseParametersFromConsole(out uint maxContainerCount, out decimal containerStorageFee)
        {
            Console.WriteLine("Введите вместительность склада - положительное целое число.");
            while (!uint.TryParse(Console.ReadLine(), out maxContainerCount) || maxContainerCount < 0)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }

            Console.WriteLine("Введите стоимость хранения контейнера на складе - положительное число.");
            while (!decimal.TryParse(Console.ReadLine(), out containerStorageFee) || containerStorageFee < 0.0m)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз!");
            }
        }

        /// <summary>
        /// Запрашивает и выполняет команды из консоли.
        /// </summary>
        /// <param name="warehouse">Склад.</param>
        private static void RequestAndExecuteComandsFromConsole(Warehouse warehouse)
        {
            ShowInfo();
            do
            {
                try
                {
                    if (!ComandType.TryParse(Console.ReadLine(), out ComandType comandNumber))
                    {
                        Console.WriteLine("Нет команды с данным номером. Попробуйте еще раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case ComandType.AddContainerToWarehouse:
                            Console.Clear();
                            AddContainerToWarehouse(warehouse);
                            Console.WriteLine();
                            ShowInfo();
                            break;

                        case ComandType.RemoveContainerById:
                            Console.Clear();
                            RemoveContainerById(warehouse);
                            Console.WriteLine();
                            ShowInfo();
                            break;

                        case ComandType.ShowInformation:
                            Console.Clear();
                            Console.WriteLine(warehouse);
                            Console.WriteLine();
                            ShowInfo();
                            break;


                        case ComandType.Exit:
                            return;

                        default:
                            Console.WriteLine("Нет команды с данным номером. Попробуйте еще раз.");
                            break;
                    }
                }

                catch (Exception)
                {
                    Console.WriteLine("Не удалось выполнить команду.");
                }
            } while (true);
        }

        /// <summary>
        /// Удаляет контейнер со склада.
        /// </summary>
        /// <param name="warehouse">Склад.</param>
        private static void RemoveContainerById(Warehouse warehouse)
        {
            Console.WriteLine("Введите идентификатор контейнера для его удаления.");
            string containerId = Console.ReadLine();
            warehouse.RemoveContainerById(containerId);
        }

        /// <summary>
        /// Добавляет контейнер на склад.
        /// </summary>
        /// <param name="warehouse">Склад.</param>
        private static void AddContainerToWarehouse(Warehouse warehouse)
        {
            Console.WriteLine("Введите ID контейнера!");
            string id = null;
            do
            {
                id = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Идентификатор не может состоять только из пробельных символов. Попробуйте ввести другой идентификатор!");
                    continue;
                }
                if (warehouse.IsContainerExistsById(id))
                {
                    Console.WriteLine("Контейнер с таким идентификатором уже существует. Попробуйте ввести другой идентификатор!");
                    continue;
                }
                break;
            }
            while (true);
            Container newContainer = new Container(id);

            string inputWishCreateBox;

            while (true)
            {
                Console.WriteLine("Если хотите добавить ящик в контейнер - введите YES, если нет - любую другую фразу.");
                Console.WriteLine("После заполнения контейнера ящиками мы попробуем поместить его на склад.");

                inputWishCreateBox = Console.ReadLine();
                if (inputWishCreateBox.ToUpper() != "YES")
                {
                    break;
                }

                var vegetableBox = CreateVegatableBox();
                newContainer.AddBox(vegetableBox);

            }

            warehouse.AddContainer(newContainer);
        }

        /// <summary>
        /// Создает ящик с овощами.
        /// </summary>
        /// <returns>Ящик с овощами.</returns>
        private static VegetableBox CreateVegatableBox()
        {
            uint weight;
            Console.WriteLine("Введите вес ящика - положительное целое число.");
            while (!uint.TryParse(Console.ReadLine(), out weight) || weight < 0)
            {
                Console.WriteLine("Неверный ввод веса ящика. Попробуйте ещё раз!");
            }

            decimal pricePerKilogram;
            Console.WriteLine("Введите цену за килограмм - положительное число.");
            while (!decimal.TryParse(Console.ReadLine(), out pricePerKilogram) || pricePerKilogram < 0.0m)
            {
                Console.WriteLine("Неверный ввод цены за килограмм ящика. Попробуйте ещё раз!");
            }

            VegetableBox newVegetableBox = new VegetableBox(weight, pricePerKilogram);
            return newVegetableBox;
        }

        /// <summary>
        /// Получает номер способа ввода.
        /// </summary>
        /// <returns>Номер способа ввода.</returns>
        private static InputType GetInputType()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Выберите способ ввода:");
            Console.WriteLine("[1]: ввод с консоли.");
            Console.WriteLine("[2]: ввод из файлов.");
            Console.ResetColor();
            do
            {
                if (!InputType.TryParse(Console.ReadLine(), out InputType inputType))
                {
                    Console.WriteLine("Команды с таким номером ввода не существует!");
                    continue;
                }
                switch (inputType)
                {
                    case InputType.Console:
                        return inputType;

                    case InputType.File:
                        return inputType;

                    default:
                        Console.WriteLine("Команды с таким номером ввода не существует!");
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Выводит на консоль информацию о доступных командах.
        /// </summary>
        private static void ShowInfo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Выберите номер желаемой команды:");
            Console.WriteLine($"[{(uint)ComandType.AddContainerToWarehouse}]: Добавление контейнера на склад при имеющейся возможности.");
            Console.WriteLine($"[{(uint)ComandType.RemoveContainerById}]: Удаление контейнера со склада по его идентификатору.");
            Console.WriteLine($"[{(uint)ComandType.ShowInformation}]: Вывод на консоль всей доступной информации о складе и его компонентах.");
            Console.WriteLine($"[{(uint)ComandType.Exit}]: Завершение работы с программой.");
            Console.ResetColor();
        }
    }
}

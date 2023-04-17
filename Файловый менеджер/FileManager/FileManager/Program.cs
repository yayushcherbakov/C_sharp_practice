using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileManager
{
    class Program
    {
        //Текущая дериктория.
        private static string _currentPath = null;
        //Поддерживаемые кодировки.
        private static string[] _supportedEncodings = { "utf8", "utf32", "ascii", "unicode", "bigendianunicode" };

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Вас приветствует Файловый менеджер!!!");
            ShowHelpInfo();
            var isNeedToExit = false;
            while (!isNeedToExit)
            {
                ShowCurrentPath();

                try
                {
                    isNeedToExit = ExecuteComand();
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Нет необходимых прав доступа в данной директории");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось выполнить команду");
                    return;
                }
            }
        }

        /// <summary>
        /// Метод для запроса команды от пользователя и ее выполнения.
        /// </summary>
        /// <returns>Возвращает true в случае, когда пользователь хочет пректарить работу с программой.</returns>
        private static bool ExecuteComand()
        {
            string comand = Console.ReadLine();

            string[] parameters = comand.Split();
            if (parameters is null || parameters.Length < 1)
            {
                Console.WriteLine("Incorrect input");
            }

            switch (parameters[0].ToLower())
            {
                case "show-drives":
                    ShowDrives(parameters);
                    return false;
                case "select-drive":
                    SelectDrive(parameters);
                    return false;
                case "show-directories":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    ShowDirectories(parameters);
                    return false;
                case "select-directory":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    SelectDirectory(parameters);
                    return false;
                case "show-files":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    ShowFileList(parameters);
                    return false;
                case "open-txt-file":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    OpenTXTFile(parameters);
                    return false;
                case "copy-file":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    CopyFile(parameters);
                    return false;
                case "move-file":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    MoveFile(parameters);
                    return false;
                case "delete-file":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    DeleteFile(parameters);
                    return false;
                case "create-txt-file":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    CreateTXTFile(parameters);
                    return false;
                case "concat-text-files":
                    if (!IsCurrentDirectorySet())
                    {
                        return false;
                    }
                    ConcatTextFiles(parameters);
                    return false;
                case "help":
                    ShowHelpInfo(parameters);
                    return false;
                case "exit":
                    return true;
                default:
                    Console.WriteLine("Incorrect input");
                    return false;
            }
        }

        /// <summary>
        /// Метод для отображения справочной информации пользователю.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Единственным параметром выступает название команды.</param>
        private static void ShowHelpInfo(string[] parameters = null)
        {
            if (!(parameters is null) && parameters.Length != 1)
            {
                Console.WriteLine("Некорректный ввод. Комада \"help\" не принемает параметнов.");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Программа поддерживает следующие команды:");

            Console.WriteLine();

            Console.WriteLine("help\tИнструкцию пользователя.");
            Console.WriteLine("пример вызова:\thelp");
            
            Console.WriteLine();

            Console.WriteLine("help\tПозволяет отобразить инструкцию пользователя.");
            Console.WriteLine("пример вызова:\thelp");

            Console.WriteLine();

            Console.WriteLine("exit\tПозволяет завершить программу.");
            Console.WriteLine("пример вызова:\texit");

            Console.WriteLine();

            Console.WriteLine("show-drives\tОтображает диски на вашем устройстве. Не имеет параметров.");
            Console.WriteLine("пример вызова:\tshow-drivers");

            Console.WriteLine();

            Console.WriteLine("select-drive\tПредоставляет возможность выбора диска.");
            Console.WriteLine("пример вызова: select-drive driveName");

            Console.WriteLine();

            Console.WriteLine("show-directories\tОтображает директории по текущему пути. Не имеет параметров.");
            Console.WriteLine("пример вызова: show-directories");

            Console.WriteLine();

            Console.WriteLine("select-directory\tОсуществляет переход в заданную директорию. " +
                "Поддерживает использования названия(в этом случае поиск директории будет вестить в текущей директории), " +
                "полного пути, относительного пути директории");
            Console.WriteLine("пример вызова: select-directory directoryName");

            Console.WriteLine();

            Console.WriteLine("show-files\tОтображает файлы по текущему пути. Не имеет параметров.");
            Console.WriteLine("пример вызова: show-files");

            Console.WriteLine();

            Console.WriteLine("open-txt-file\tПозволяет отобразить на содержимое файла формата txt. " +
                "Поддерживаемые кодировки - UTF8, UTF32, ASCII, Unicode, BigeEndianUnicode. " +
                "Первым параметром идет кодировка файла. Затем путь к нему. " +
                "По умолчанию кодировка - UTF8. " +
                "Поддерживает использования названия(в этом случае поиск файла будет осуществляться в текущей директории), " +
                "полного пути, относительного пути файла.");
            Console.WriteLine("пример 1 вызова: open-txt-file fileName");
            Console.WriteLine("пример 2 вызова: open-txt-file UTF8 fileName");

            Console.WriteLine();

            Console.WriteLine("copy-file\tПозволяет скопировать файл. " +
                "Первым параметром идет путь к файлу. Затем директория назвачения. " +
                "Поддерживает использования названия(в этом случае поиск файла будет осуществляться в текущей директории), " +
                "полного пути, относительного пути файла. Директория назвачения также может быть как полным путем, так и относительным.");
            Console.WriteLine("пример вызова: copy-file fileName directory");

            Console.WriteLine();

            Console.WriteLine("move-file\tПозволяет переместить файл. " +
                "Первым параметром идет путь к файлу. Затем директория назвачения. " +
                "Поддерживает использования названия(в этом случае поиск файла будет осуществляться в текущей директории), " +
                "полного пути, относительного пути файла. Директория назвачения также может быть как полным путем, так и относительным.");
            Console.WriteLine("пример вызова: move-file fileName directory");

            Console.WriteLine();

            Console.WriteLine("delete-file\tПозволяет удалить файл. " +
                "Едиственным параметром идет путь к файлу." +
                "Поддерживает использования названия(в этом случае поиск файла будет осуществляться в текущей директории), " +
                "полного пути, относительного пути файла.");
            Console.WriteLine("пример вызова: delete-file fileName directory");

            Console.WriteLine();

            Console.WriteLine("create-txt-file\tПозволяет создать простой текстовый файл. " +
                "Поддерживаемые кодировки - UTF8, UTF32, ASCII, Unicode, BigeEndianUnicode. " +
                "Первым параметром идет кодировка файла. Затем путь к нему. " +
                "По умолчанию кодировка - UTF8. " +
                "Поддерживает использования названия(в этом случае файл будет создан в текущей директории), " +
                "полного пути, относительного пути файла.");
            Console.WriteLine("пример 1 вызова: create-txt-file fileName");
            Console.WriteLine("пример 2 вызова: create-txt-file UTF8 fileName");

            Console.WriteLine();

            Console.WriteLine("concat-text-files\tОбъединяет несколько файлов в один " +
                "Поддерживаемые кодировки файлов для конкатенации - UTF8, UTF32, ASCII, Unicode, BigeEndianUnicode. " +
                "Результирующий файл имеет кодировку - UTF8. " +
                "Поддерживает использования названия(в этом случае файл будет создан в текущей директории), " +
                "полного пути, относительного пути файла.");
            Console.WriteLine("пример вызова: concat-text-files");

            Console.WriteLine();

            Console.ResetColor();
        }

        /// <summary>
        /// Метод позволяет выбрать диск.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. 
        /// Затем идет название диска.</param>
        private static void SelectDrive(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine("Некорректный ввод. Данная команда принимает однин обязательный входной параметр - название диска.");
            }
            var driveBuilder = new StringBuilder();
            for (int i = 1; i < parameters.Length; i++)
            {
                driveBuilder.Append(parameters[i]);
                if (i != parameters.Length - 1)
                {
                    driveBuilder.Append(" ");
                }
            }
            var userDirectory = driveBuilder.ToString();
            var userDirectoryInfo = new DirectoryInfo(Path.Combine(_currentPath, userDirectory));
            if (userDirectoryInfo.Exists)
            {
                AddDirectoryToPath(userDirectoryInfo.FullName);
            }
            else
            {
                Console.WriteLine($"Директория с названием {userDirectory} не обнаружена");
            }
        }

        // <summary>
        /// Метод позволяет отобразить диски.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Единственным параметром выступает название команды.</param>
        private static void ShowDrives(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine("Incorrect input");
            }
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                Console.WriteLine(drive.Name);
            };
        }

        /// <summary>
        /// Метод позволяет отобразить дочерние директории текущей диктории
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. 
        /// Затем идет название директории.</param>
        private static void ShowDirectories(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine("Incorrect input");
            }
            var directoryInfo = new DirectoryInfo(_currentPath);
            var childDirectories = directoryInfo.EnumerateDirectories().ToList();
            if (childDirectories.Count == 0)
            {
                Console.WriteLine("Текущая дериктория не содержит дочерних директорий");
            }
            foreach (var directory in directoryInfo.EnumerateDirectories())
            {
                Console.WriteLine(directory.Name);
            };
        }

        /// <summary>
        /// Метод позволяет выбрать новую директорию. Допускаются название, полный путь, относительный путь директории.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. 
        /// Затем идет название директории.</param>
        private static void SelectDirectory(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine("Некорректный ввод. Данная команда принимает однин обязательный входной параметр - название директории.");
            }
            var directoryBuilder = new StringBuilder();
            for (int i = 1; i < parameters.Length; i++)
            {
                directoryBuilder.Append(parameters[i]);
                if (i != parameters.Length - 1)
                {
                    directoryBuilder.Append(" ");
                }
            }
            var userDirectory = directoryBuilder.ToString();
            var userDirectoryInfo = new DirectoryInfo(Path.Combine(_currentPath, userDirectory));
            if (userDirectoryInfo.Exists)
            {
                AddDirectoryToPath(userDirectoryInfo.FullName);
            }
            else
            {
                Console.WriteLine($"Диск с названием {parameters[1]} не обнаружен");
            }
        }

        /// <summary>
        /// Метод для отображения текущей директории пользователю.
        /// </summary>
        private static void ShowCurrentPath()
        {
            if (!(_currentPath is null))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Текущая директория: {_currentPath}");
                Console.ResetColor();
            }

        }

        /// <summary>
        /// Метод для отображения списка файлов текущей директории.
        /// </summary>
        /// <param name="parameters">Команда имеет оди параметр - название команды.</param>
        private static void ShowFileList(string[] parameters)
        {
            if (parameters.Length != 1)
            {
                Console.WriteLine("Некорректный ввод. Данная команда не принемает параметров");
            }

            var currentDirectoryInfo = new DirectoryInfo(_currentPath);
            var files = currentDirectoryInfo.GetFiles();
            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
            }
        }

        /// <summary>
        /// Метод для перемещения файла.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. 
        /// Затем идет название файла и директория, в которую будет скопирован файл.
        /// Допускается полный путь.</param>
        private static void CopyFile(string[] parameters)
        {
            if (parameters.Length < 3)
            {
                Console.WriteLine("Некорректный ввод. Данная команда принемает два параметра");
            }

            var filePath = Path.Combine(_currentPath, parameters[1]);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не обнаружен");
                return;
            }

            var directoryBuilder = new StringBuilder();
            for (int i = 2; i < parameters.Length; i++)
            {
                directoryBuilder.Append(parameters[i]);
                if (i != parameters.Length - 1)
                {
                    directoryBuilder.Append(" ");
                }
            }
            var userDirectoryInfo = new DirectoryInfo(Path.Combine(_currentPath, directoryBuilder.ToString()));
            if (!userDirectoryInfo.Exists)
            {
                Console.WriteLine($"Директория {userDirectoryInfo.FullName} не обнаружена");
                return;
            }

            var newFilePath = Path.Combine(userDirectoryInfo.FullName, Path.GetFileName(filePath));
            if (File.Exists(newFilePath))
            {
                Console.WriteLine($"Файл с названием {newFilePath} уже существует в директории назначения");
                return;
            }
            try
            {
                File.Copy(filePath, newFilePath);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав записи в директорию назначения");
                Console.WriteLine($"Файл не был скопирован");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл не был скопирован");
                return;
            }
            Console.WriteLine("Файл скопирован");
        }

        /// <summary>
        /// Метод для перемещения файла.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. Затем идет название файла и директория для перемещения.
        /// Допускается полный путь.</param>
        private static void MoveFile(string[] parameters)
        {
            if (parameters.Length < 3)
            {
                Console.WriteLine("Некорректный ввод. Данная команда принемает два параметра");
            }

            var filePath = Path.Combine(_currentPath, parameters[1]);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не обнаружен");
                return;
            }

            var directoryBuilder = new StringBuilder();
            for (int i = 2; i < parameters.Length; i++)
            {
                directoryBuilder.Append(parameters[i]);
                if (i != parameters.Length - 1)
                {
                    directoryBuilder.Append(" ");
                }
            }
            var userDirectoryInfo = new DirectoryInfo(Path.Combine(_currentPath, directoryBuilder.ToString()));
            if (!userDirectoryInfo.Exists)
            {
                Console.WriteLine($"Директория {userDirectoryInfo.FullName} не обнаружена");
                return;
            }

            var newFilePath = Path.Combine(userDirectoryInfo.FullName, Path.GetFileName(filePath));
            if (File.Exists(newFilePath))
            {
                Console.WriteLine($"Файл с названием {newFilePath} уже существует в директории назначения");
                return;
            }
            try
            {
                File.Move(filePath, newFilePath);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав записи в директорию назначения");
                Console.WriteLine($"Файл не был перемещен");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл не был перемещен");
                return;
            }
            Console.WriteLine("Файл перемещен");
        }

        /// <summary>
        /// Метод для удаления файла.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. Затем идет название файла. Допускается полный путь.</param>
        private static void DeleteFile(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine("Некорректный ввод. Данная команда принемает один обязательный параметр");
            }

            var filePathBuilder = new StringBuilder();
            for (int i = 1; i < parameters.Length; i++)
            {
                filePathBuilder.Append(parameters[i]);
                if (i != parameters.Length - 1)
                {
                    filePathBuilder.Append(" ");
                }
            }

            var filePath = Path.Combine(_currentPath, filePathBuilder.ToString());
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не обнаружен");
                return;
            }

            try
            {
                File.Delete(filePath);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав для удаления в данной директории");
                Console.WriteLine($"Файл не был перемещен");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл не был удален");
                return;
            }
            Console.WriteLine("Файл удален");
        }

        /// <summary>
        /// Метод для добавления директории к текущему выбранному пути.
        /// </summary>
        /// <param name="directory"></param>
        private static void AddDirectoryToPath(string directory)
        {
            if (_currentPath is null)
            {
                _currentPath = Path.Combine(directory);
                return;
            }
            _currentPath = Path.Combine(_currentPath, directory);
        }

        /// <summary>
        /// Метод для создания открытия файла формата txt.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. Затем идет название файла. Допускается полный путь.</param>
        private static void OpenTXTFile(string[] parameters)
        {
            var filePath = GetPathFromTXTComandParameters(parameters);
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден");
                return;
            }

            string fileText = null;
            try
            {
                switch (parameters[1].ToLower())
                {
                    case "utf8":
                        fileText = File.ReadAllText(filePath, Encoding.UTF8);
                        break;
                    case "utf32":
                        fileText = File.ReadAllText(filePath, Encoding.UTF32);
                        break;
                    case "ascii":
                        fileText = File.ReadAllText(filePath, Encoding.ASCII);
                        break;
                    case "unicode":
                        fileText = File.ReadAllText(filePath, Encoding.Unicode);
                        break;
                    case "bigendianunicode":
                        fileText = File.ReadAllText(filePath, Encoding.BigEndianUnicode);
                        break;
                    default:
                        fileText = File.ReadAllText(filePath, Encoding.UTF8);
                        break;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав чтения в данной директории");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл не может быть считан");
                return;
            }
            Console.WriteLine(fileText);
        }

        /// <summary>
        /// Метод для создания текстового файла формата txt.
        /// </summary>
        /// <param name="parameters">Массив параметров команды. 
        /// Первым параметром выступает название команды. Затем идет название файла. Допускается полный путь.</param>
        private static void CreateTXTFile(string[] parameters)
        {
            var filePath = GetPathFromTXTComandParameters(parameters);
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            if (File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} уже существует");
                return;
            }

            Console.WriteLine("Введите содержимое файла. Enter завершает ввод.");
            string fileText = Console.ReadLine();

            try
            {
                switch (parameters[1].ToLower())
                {
                    case "utf8":
                        File.WriteAllText(filePath, fileText, Encoding.UTF8);
                        break;
                    case "utf32":
                        File.WriteAllText(filePath, fileText, Encoding.UTF32);
                        break;
                    case "ascii":
                        File.WriteAllText(filePath, fileText, Encoding.ASCII);
                        break;
                    case "unicode":
                        File.WriteAllText(filePath, fileText, Encoding.Unicode);
                        break;
                    case "bigendianunicode":
                        File.WriteAllText(filePath, fileText, Encoding.BigEndianUnicode);
                        break;
                    default:
                        File.WriteAllText(filePath, fileText, Encoding.UTF8);
                        break;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав записи в дунную директорию");
                Console.WriteLine($"Файл не был сохранен");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл не был сохранен");
                return;
            }

            Console.WriteLine($"Файл {filePath} сохранен");
        }

        private static void ConcatTextFiles(string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine("Некорректный ввод. Команда вызывается без параметров");
                return;
            }
            Console.WriteLine("Введите название результирующего файла. Для прерывания выполнения команды введите \"exit\"");
            string resultFilePath = null;
            do
            {
                var userInput = Console.ReadLine();
                if (userInput == "exit")
                {
                    return;
                }
                if (!userInput.ToLower().EndsWith(".txt"))
                {
                    Console.WriteLine("Результирующий файл должен иметь расширение \".txt\". Попробуйте еще раз");
                    continue;
                }
                resultFilePath = Path.Combine(_currentPath, userInput);
                if (File.Exists(resultFilePath))
                {
                    Console.WriteLine("Файл с таким названием уже существует в выбранной директории. Выберите другое название файла");
                    resultFilePath = null;
                    continue;
                }
            }
            while (resultFilePath is null);

            Console.WriteLine("Введите файлы для конкотенации. Каждый новый файл вводите с новой строки. Для завершения выбора файлов введите \"done\". Для прерывания выполнения команды введите \"exit\"");
            var concatFiles = new List<string>();
            do
            {
                var userInput = Console.ReadLine();
                if (userInput == "exit")
                {
                    return;
                }
                if (userInput == "done" && concatFiles.Count < 2)
                {
                    Console.WriteLine("Для конкотенации необходимо минимум два файла. Введите еще несколько названий файлов");
                    continue;
                }
                if (userInput == "done")
                {
                    break;
                }
                if (!userInput.ToLower().EndsWith(".txt"))
                {
                    Console.WriteLine("Файл должен иметь расширение \".txt\". Попробуйте еще раз");
                    continue;
                }
                var newUserFilePath = Path.Combine(_currentPath, userInput);
                if (!File.Exists(newUserFilePath))
                {
                    Console.WriteLine("Файла с таким названием не существует в выбранной директории. Выберите другое название файла");
                    continue;
                }
                concatFiles.Add(newUserFilePath);
            }
            while (true);

            Console.WriteLine("Выбраны следующие файлы:");
            foreach (var file in concatFiles)
            {
                Console.WriteLine(file);
            }

            foreach (var file in concatFiles)
            {
                var sdf = GetEncoding(file);
            }

            Console.WriteLine($"Результат будет записан в файл {resultFilePath}");

            var fileBuilder = new StringBuilder();
            for (int i = 0; i < concatFiles.Count; i++)
            {
                try
                {
                    fileBuilder.Append(File.ReadAllText(concatFiles[i], GetEncoding(concatFiles[i])));
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Нет прав чтения файла {concatFiles[i]}");
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не удалось выполнить команду");
                    return;
                }

                if (i != parameters.Length - 1)
                {
                    fileBuilder.Append("\n");
                }
            }

            var content = fileBuilder.ToString();
            try
            {
                File.WriteAllText(resultFilePath, content, Encoding.UTF8);
                Console.WriteLine(content);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав записи в дунную директорию");
                Console.WriteLine($"Файл не был сохранен");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Файл не был сохранен");
                return;
            }

            Console.WriteLine($"Файл {resultFilePath} сохранен");
        }

        /// <summary>
        /// Метод позволяет определить кодировку файла по метке порядка байтов.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>Возвращает кодировку файла. Кодировкой по умолчанию считается ASCII</returns>
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
        /// Метод позволяет считать 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GetPathFromTXTComandParameters(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine("Некорректный ввод. Название файла - обязательный параметр");
            }
            var fileNamePosition = 1;

            //Проверка наличия кодировки вторым параметром.
            var isParametersHasEncoding = _supportedEncodings.Any(x => x == parameters[1].ToLower());
            if (isParametersHasEncoding)
            {
                if (parameters.Length < 3)
                {
                    Console.WriteLine("Некорректный ввод. Название файла - обязательный параметр");
                    return null;
                }
                fileNamePosition = 2;
            }

            //Оставшиеся параметры являются именем файла. Обединяем их в одну строку.
            var filePathBuilder = new StringBuilder();
            for (int i = fileNamePosition; i < parameters.Length; i++)
            {
                filePathBuilder.Append(parameters[i]);
                if (i != parameters.Length - 1)
                {
                    filePathBuilder.Append(" ");
                }
            }
            var userFilePath = filePathBuilder.ToString();

            if (!userFilePath.ToLower().EndsWith(".txt"))
            {
                Console.WriteLine("Некорректный ввод. Файл должен иметь расширение \".txt\"");
                return null;
            }
            return Path.Combine(_currentPath, userFilePath);
        }

        /// <summary>
        /// Метод проверяет состояние поля _currentPath.
        /// </summary>
        /// <returns>Возвращает false, если поле не установлено.</returns>
        private static bool IsCurrentDirectorySet()
        {
            if (_currentPath is null)
            {
                Console.WriteLine("Команда недоступна. Сначала необходимо выбрать диск.");
                return false;
            }
            return true;
        }
    }
}

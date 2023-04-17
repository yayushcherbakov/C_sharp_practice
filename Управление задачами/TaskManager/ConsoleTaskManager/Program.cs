using System;
using TaskManager.Tasks.Enums;

namespace ConsoleTaskManager
{
    class Program
    {
        /// <summary>
        /// Ввод пользователя.
        /// </summary>
        static string[] input;

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main(string[] args)
        {
            var taskManager = new TaskManager();

            taskManager.RestoreUsersFromFile();
            taskManager.RestoreProjectsFromFile();

            try
            {
                do
                {
                    ShowMainComandsList();

                    if (!Comands.TryParse(Console.ReadLine(), out Comands comandNumber))
                    {
                        Console.WriteLine("Не удалось выполнить команду с данным номерkом. Попробуйте ещё раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case Comands.WorkWithUser:
                            Console.Clear();
                            ShowUserComand();
                            ChooseUserComand(taskManager);
                            break;

                        case Comands.WorkWithProject:
                            Console.Clear();
                            ShowProjectComandsList();
                            ChooseProjectComand(taskManager);
                            break;

                        case Comands.WorkWithTasksInProject:
                            Console.Clear();
                            ShowTaskComandsList();
                            ChooseTaskComand(taskManager);
                            break;

                        case Comands.WorkWithEpicTasks:
                            Console.Clear();
                            ShowEpicComandsList();
                            ChooseEpicTaskComand(taskManager);
                            break;

                        case Comands.SaveAppState:
                            Console.Clear();
                            taskManager.SaveStateToFile(taskManager);
                            break;

                        case Comands.Exit:
                            Console.Clear();
                            taskManager.SaveStateToFile(taskManager);
                            return;

                        default:
                            Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                            break;
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Перенаправляет на выполнение выбранной команды c задачей типа Epic.
        /// </summary>
        /// <param name="taskManager">Объект класса TaskManager.</param>
        private static void ChooseEpicTaskComand(TaskManager taskManager)
        {
            try
            {
                do
                {
                    if (!Comands.TryParse(Console.ReadLine(), out Comands comandNumber))
                    {
                        Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case Comands.AddSubtaskToEpic:
                            Console.WriteLine("Введите одной строкой название существующего проекта название задачи типа Epic и название добавляемой задачи типа Story или Task.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 3)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.AddTaskToEpic(input[0], input[1], input[2]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.RemoveSubtaskFromEpic:
                            Console.WriteLine("Введите одной строкой название существующего проекта название задачи типа Epic и название удаляемой подзадачи.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 3)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.RemoveSubtaskFromEpic(input[0], input[1], input[2]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ReturnToMainMenu:
                            Console.Clear();
                            return;

                        default:
                            Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                            break;
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Перенаправляет на выполнение выбранной команды c задачей.
        /// </summary>
        /// <param name="taskManager">Объект класса TaskManager.</param>
        private static void ChooseTaskComand(TaskManager taskManager)
        {
            try
            {
                do
                {
                    if (!Comands.TryParse(Console.ReadLine(), out Comands comandNumber))
                    {
                        Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case Comands.AddNewTask:
                            Console.WriteLine("Введите одной строкой название существующего проекта и придуманное название задачи, которую необходимо в него добавить.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 2)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.AddNewTaskToProject(input[0], input[1]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.AssignUser:
                            Console.WriteLine("Введите одной строкой имя пользователя, название проекта и название задачи, на которую необходимо подписать пользователя.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 3)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.AddUserToTask(input[0], input[1], input[2]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.UnassignUser:
                            Console.WriteLine("Введите одной строкой имя пользователя, название проекта и название задачи, из которой необходимо удалить пользователя.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 3)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.UnssignUserFromTask(input[0], input[1], input[2]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ChangeTaskStatus:
                            Console.WriteLine("Введите одной строкой название проекта и название задачи, статус которой необходимо изменить.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 2)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.ChangeTaskStatus(input[0], input[1]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ShowTasksList:
                            Console.WriteLine("Введите название проекта, список задач которого необходимо показать.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 1)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.ShowTaskList(input[0]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.SortStatus:
                            Console.WriteLine("Введите название проекта для группировки задач по статусу.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 1)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.SortStatus(input[0]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.RemoveTaskFromProject:
                            Console.WriteLine("Введите одной строкой название проекта и название задачи, которуе нужно удалить из проекта.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 2)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.RemoveTaskFromProject(input[0], input[1]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ReturnToMainMenu:
                            Console.Clear();
                            return;

                        default:
                            Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                            break;
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Перенаправляет на выполнение выбранной команды c пользователем.
        /// </summary>
        /// <param name="taskManager">Объект класса TaskManager.</param>
        private static void ChooseUserComand(TaskManager taskManager)
        {
            try
            {
                do
                {
                    if (!Comands.TryParse(Console.ReadLine(), out Comands comandNumber))
                    {
                        Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case Comands.CreateUser:
                            Console.WriteLine("Введите имя пользователя, которого хотите создать.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 1)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.CreateUser(input[0]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ShowUsersList:
                            Console.WriteLine("Список пользователей:");
                            taskManager.ShowUsers();
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.RemoveUser:
                            Console.WriteLine("Введите имя пользователя, которого хотите удалить.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 1)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.RemoveUser(input[0]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ReturnToMainMenu:
                            Console.Clear();
                            return;

                        default:
                            Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                            break;
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Перенаправляет на выполнение выбранной команды c проектом.
        /// </summary>
        /// <param name="taskManager">Объект класса TaskManager.</param>
        private static void ChooseProjectComand(TaskManager taskManager)
        {
            try
            {
                do
                {
                    if (!Comands.TryParse(Console.ReadLine(), out Comands comandNumber))
                    {
                        Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                        continue;
                    }

                    switch (comandNumber)
                    {
                        case Comands.CreateProject:
                            Console.WriteLine("Введите одной строкой название создаваемого проекта и максимальное количество задач в нём.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 2)
                                throw new ArgumentException("Неверное количество аргументов.");
                            if (!int.TryParse(input[1], out int result))
                                throw new ArgumentException("Вторым аргументом должно быть натуральное число.");
                            taskManager.CreateProject(input[0], int.Parse(input[1]));
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ShowProjectsList:
                            Console.WriteLine("Список проектов:");
                            taskManager.ShowProjectList();
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.RenameProject:
                            Console.WriteLine("Введите одной строкой старое и новое название проекта.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 2)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.RenameProject(input[0], input[1]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.RemoveProject:
                            Console.WriteLine("Введите название проекта, который хотите удалить.");
                            input = Console.ReadLine().Split(' ');
                            if (input.Length != 1)
                                throw new ArgumentException("Неверное количество аргументов.");
                            taskManager.RemoveProject(input[0]);
                            Console.WriteLine("Выберите номер команды из текущего меню.");
                            break;

                        case Comands.ReturnToMainMenu:
                            Console.Clear();
                            return;

                        default:
                            Console.WriteLine("Не удалось выполнить команду с данным номером. Попробуйте ещё раз.");
                            break;
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Выводит информацию о командах главного меню на консоль.
        /// </summary>
        private static void ShowMainComandsList()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Номера команд:");
            Console.WriteLine("Выберите [1] для работы с пользователями.");
            Console.WriteLine("Выберите [2] для работы с проектами.");
            Console.WriteLine("Выберите [3] для работы с задачами в проекте.");
            Console.WriteLine("Выберите [4] для задач типа Epic.");
            Console.WriteLine("Выберите [5] для сохранения состояния приложения.");
            Console.WriteLine("Выберите [0] для выхода из приложения.");
            Console.WriteLine("Для успешного сохранения состояния приложения пользуйтесь командами [5] и [0](при выходе).");
            Console.ResetColor();
        }

        /// <summary>
        /// Выводит информацию о командах для работы с Epic на консоль.
        /// </summary>
        private static void ShowEpicComandsList()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Номера команд:");
            Console.WriteLine("Выберите [20] для назначения подзадач в Epic.");
            Console.WriteLine("Выберите [21] для удаления подзадач в Epic.");
            Console.WriteLine("Выберите [666] для возвращения в главное меню.");
            Console.ResetColor();
        }


        /// <summary>
        /// Выводит информацию о командах для работы с задачами в проекте на консоль.
        /// </summary>
        private static void ShowTaskComandsList()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Номера команд:");
            Console.WriteLine("Выберите [13] для добавления новой задачи в проект.");
            Console.WriteLine("Выберите [14] для назначения исполнителя для выполнения задачи.");
            Console.WriteLine("Выберите [15] для удаления исполнителя из выполнения задачи.");
            Console.WriteLine("Выберите [16] для изменения статуса задачи.");
            Console.WriteLine("Выберите [17] для просмотра списка задач.");
            Console.WriteLine("Выберите [18] для группировки задач по статусу.");
            Console.WriteLine("Выберите [19] для удаления задачи из проекта.");
            Console.WriteLine("Выберите [666] для возвращения в главное меню.");
            Console.ResetColor();
        }


        /// <summary>
        /// Выводит информацию о командах для работы с проектами на консоль.
        /// </summary>
        private static void ShowProjectComandsList()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Номера команд:");
            Console.WriteLine("Выберите [9] для создания проекта.");
            Console.WriteLine("Выберите [10] для просмотра списка проектов.");
            Console.WriteLine("Выберите [11] для изменения названия проекта.");
            Console.WriteLine("Выберите [12] для удаления проекта.");
            Console.WriteLine("Выберите [666] для возвращения в главное меню.");
            Console.ResetColor();
        }

        /// <summary>
        /// Выводит информацию о командах для работы с пользователями на консоль.
        /// </summary>
        private static void ShowUserComand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Номера команд:");
            Console.WriteLine("Выберите [6] для создания пользователя.");
            Console.WriteLine("Выберите [7] для просмотра списка пользователей.");
            Console.WriteLine("Выберите [8] для удаления пользователя.");
            Console.WriteLine("Выберите [666] для возвращения в главное меню.");
            Console.ResetColor();
        }
    }
}

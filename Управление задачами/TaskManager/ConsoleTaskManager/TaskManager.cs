using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskManager;
using TaskManager.Tasks;
using TaskManager.Tasks.Base;
using TaskManager.Tasks.Enums;
using TaskManager.Tasks.Interfaces;

namespace ConsoleTaskManager
{
    public class TaskManager
    {
        private readonly string saveDirectoryName = $"save";
        private readonly string usersSaveFileName = $"users.save";
        private readonly string projectsSaveFileName = $"projects.save";
        private readonly string epicsSaveFileName = $"epics.save";
        private readonly string bugsSaveFileName = $"bugs.save";
        private readonly string storiesSaveFileName = $"stories.save";
        private readonly string tasksSaveFileName = $"tasks.save";

        /// <summary>
        /// Список пользователей.
        /// </summary>
        public List<User> users = new List<User>();

        /// <summary>
        /// Список проектов.
        /// </summary>
        public List<Project> projects = new List<Project>();

        /// <summary>
        /// Выводит на консоль информацию о задачах в проекте.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        public void ShowTaskList(string projectName)
        {
            foreach (var task in FindProject(projectName).tasks)
            {
                Console.WriteLine(task.ToString());
            }
        }

        /// <summary>
        /// Удаляет задачу из проекта.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="taskName">Название удаляемой задачи.</param>
        public void RemoveTaskFromProject(string projectName, string taskName)
        {
            var project = FindProject(projectName);
            var task = FindTask(projectName, taskName);

            project.tasks.Remove(task);

            Console.WriteLine($"Задача {taskName} успешно удалена из проекта {projectName}.");
        }

        /// <summary>
        /// Выводит отсортированный список задач по статусам на консоль.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        public void SortStatus(string projectName)
        {
            var project = FindProject(projectName);
            string output;

            output = "Задачи со статусом \"Открытая задача\":";

            foreach (var task in project.tasks)
            {
                if (task.Status == TaskStatus.Opened)
                {
                    output += $" {task.Name},";
                }
            }

            Console.WriteLine(output += "\b.");

            output = "Задачи со статусом \"Задача в работе\":";

            foreach (var task in project.tasks)
            {
                if (task.Status == TaskStatus.InProgres)
                {
                    output += $" {task.Name},";
                }
            }

            Console.WriteLine(output += "\b.");

            output = "Задачи со статусом \"Завершенная задача\":";

            foreach (var task in project.tasks)
            {
                if (task.Status == TaskStatus.Closed)
                {
                    output += $" {task.Name},";
                }
            }

            Console.WriteLine(output += "\b.");
        }

        /// <summary>
        /// Изменяет статус задачи.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="taskName">Название задачи.</param>
        public void ChangeTaskStatus(string projectName, string taskName)
        {
            Console.WriteLine("Выберите присваиваемый статус:");
            Console.WriteLine("[0] - открытая задача.");
            Console.WriteLine("[1] - задача в работе.");
            Console.WriteLine("[2] - завершённая задача.");

            do
            {
                try
                {
                    switch (Enum.Parse(typeof(TaskStatus), Console.ReadLine()))
                    {
                        case TaskStatus.Opened:
                            FindTask(projectName, taskName).Status = TaskStatus.Opened;
                            Console.WriteLine($"Статус задачи {taskName} успешно изменён.");
                            return;

                        case TaskStatus.InProgres:
                            FindTask(projectName, taskName).Status = TaskStatus.InProgres;
                            Console.WriteLine($"Статус задачи {taskName} успешно изменён.");
                            return;

                        case TaskStatus.Closed:
                            FindTask(projectName, taskName).Status = TaskStatus.Closed;
                            Console.WriteLine($"Статус задачи {taskName} успешно изменён.");
                            return;

                        default:
                            throw new ArgumentException("Тип статуса введён неверно. Попробуйте ещё раз.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

        /// <summary>
        /// Открепляет пользователя от задачи.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="taskName">Название задачи.</param>
        public void UnssignUserFromTask(string userName, string projectName, string taskName)
        {
            var task = FindTask(projectName, taskName);

            if (!(task is IAssignable))
            {
                throw new ArgumentException("У задачи типа Epic нет исполнителей. Невозможно выполнить операцию.");
            }

            ((IAssignable)task).UnassignUsers(FindUser(userName));

            Console.WriteLine($"Пользователь {userName} был успешно откреплён от задачи {taskName} проекта {projectName}.");
        }

        /// <summary>
        /// Прикрепляет пользователя к задаче.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="taskName">Название задачи.</param>
        public void AddUserToTask(string userName, string projectName, string taskName)
        {
            var task = FindTask(projectName, taskName);

            if (!(task is IAssignable))
            {
                throw new ArgumentException("Нельзя прикрепить пользователя к задаче типа Epic.");
            }

            ((IAssignable)task).AssignUsers(FindUser(userName));

            Console.WriteLine($"Пользователь {userName} был успешно прикреплён к задаче {taskName} проекта {projectName}.");
        }

        /// <summary>
        /// Добавляет подзадачу в список подзадач Epic.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="EpicTaskName">Название задачи типа Epic.</param>
        /// <param name="subtaskName">Название подзадачи.</param>
        public void AddTaskToEpic(string projectName, string EpicTaskName, string subtaskName)
        {
            var currentProject = FindProject(projectName);
            var epicTask = FindTask(projectName, EpicTaskName);
            var subtask = FindTask(projectName, subtaskName);

            if (!(epicTask is Epic))
            {
                throw new ArgumentException("Подзадачи могут быть только у типа Epic.");
            }

            ((Epic)epicTask).AddTask(subtask);

            //currentProject.tasks.Remove(subtask);

            Console.WriteLine($"Задача {subtaskName} успешно была помещена в {EpicTaskName} в проекте {projectName}.");
        }

        /// <summary>
        /// Удаляет подзадачу из списка подзадач Epic.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="EpicTaskName">Название задачи типа Epic.</param>
        /// <param name="subtaskName">Название подзадачи.</param>
        public void RemoveSubtaskFromEpic(string projectName, string EpicTaskName, string subtaskName)
        {
            var epicTask = FindTask(projectName, EpicTaskName);
            var subtask = FindTask(projectName, subtaskName);

            if (!(epicTask is Epic))
            {
                throw new ArgumentException("Данный тип не является Epic.");
            }

            ((Epic)epicTask).RemoveTask(subtask);

            Console.WriteLine($"Задача {subtaskName} успешно была удалена из {EpicTaskName} в проекте {projectName}.");
        }

        /// <summary>
        /// Находит заданную задачу в заданном проекте по их названиям.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="taskName">Название задачи.</param>
        /// <returns>Задача из проекта.</returns>
        private BaseTask FindTask(string projectName, string taskName)
        {
            BaseTask task;

            foreach (var item in FindProject(projectName).tasks)
            {
                if (item.Name == taskName)
                {
                    return task = item;
                }
            }

            throw new ArgumentException($"Задача с названием {taskName} не была найдена.");
        }

        /// <summary>
        /// Находит заданный проект по названию.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <returns>Проект.</returns>
        private Project FindProject(string projectName)
        {
            Project project;

            foreach (var item in projects)
            {
                if (item.Name == projectName)
                {
                    return project = item;
                }
            }

            throw new ArgumentException($"Проект с названием {projectName} не был найден.");
        }

        /// <summary>
        /// Находит заданного пользователя по имени.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Пользователь.</returns>
        private User FindUser(string userName)
        {
            User user;

            foreach (var item in users)
            {
                if (item.Name == userName)
                {
                    return user = item;
                }
            }

            throw new ArgumentException($"Пользователь с именем {userName} не был найден.");
        }


        /// <summary>
        /// Выводит на консоль информацию о проектах.
        /// </summary>
        public void ShowProjectList()
        {
            foreach (var item in projects)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Добавляет новую задачу в проект.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        /// <param name="taskName">Название задачи.</param>
        public void AddNewTaskToProject(string projectName, string taskName)
        {
            foreach (var item in projects)
            {
                if (item.Name == projectName)
                {
                    Console.WriteLine("Выберите тип задачи, которую необходимо добавить:");
                    Console.WriteLine("[0] - Epic");
                    Console.WriteLine("[1] - Story");
                    Console.WriteLine("[2] - Task");
                    Console.WriteLine("[3] - Bug");

                    do
                    {
                        try
                        {
                            switch (Enum.Parse(typeof(TasksType), Console.ReadLine()))
                            {
                                case TasksType.Epic:
                                    item.AddTask(new Epic(taskName));
                                    Console.WriteLine($"Задача с названием {taskName} и типом Epic была успешно добавлена в проект {projectName}.");
                                    return;

                                case TasksType.Story:
                                    item.AddTask(new Story(taskName));
                                    Console.WriteLine($"Задача с названием {taskName} и типом Story была успешно добавлена в проект {projectName}.");
                                    return;

                                case TasksType.Task:
                                    item.AddTask(new Task(taskName));
                                    Console.WriteLine($"Задача с названием {taskName} и типом Task была успешно добавлена в проект {projectName}.");
                                    return;

                                case TasksType.Bug:
                                    item.AddTask(new Bug(taskName));
                                    Console.WriteLine($"Задача с названием {taskName} и типом Bug была успешно добавлена в проект {projectName}.");
                                    return;

                                default:
                                    throw new ArgumentException("Тип задачи введён неверно. Попробуйте ещё раз.");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "Достигнуто максимальное количество задач. Невозможно добавить новую.")
                            {
                                Console.WriteLine(ex.Message);
                                return;
                            }

                            Console.WriteLine(ex.Message);
                        }

                    } while (true);
                }
            }

            throw new ArgumentException("Не удалось найти такую задачу или проект.");
        }

        /// <summary>
        /// Создаёт проект.
        /// </summary>
        /// <param name="name">Имя проекта.</param>
        /// <param name="maxTasksCount">Максимальное количество задач в проекте.</param>
        public void CreateProject(string name, int maxTasksCount)
        {
            projects.Add(new Project(name, maxTasksCount));
            Console.WriteLine($"Проект с названием {name} и максимальным количеством задач {maxTasksCount} успешно создан.");
        }

        /// <summary>
        /// Удаляет проект.
        /// </summary>
        /// <param name="projectName">Название проекта.</param>
        public void RemoveProject(string projectName)
        {
            foreach (var item in projects)
            {
                if (item.Name == projectName)
                {
                    projects.Remove(item);
                    Console.WriteLine($"Проект с названием {projectName} был успешно удалён.");
                    return;
                }
            }

            throw new ArgumentException($"Проект с названием {projectName} не был найден. Удаление не произошло.");
        }

        /// <summary>
        /// Изменяет название проекта.
        /// </summary>
        /// <param name="oldName">Старое название.</param>
        /// <param name="newName">Новое название.</param>
        public void RenameProject(string oldName, string newName)
        {
            foreach (var item in projects)
            {
                if (item.Name == oldName)
                {
                    item.Name = newName;
                    Console.WriteLine($"Проект с именем {oldName} был изменён на {newName}.");
                    return;
                }
            }

            throw new ArgumentException($"Проект с именем {oldName} не был найден.");
        }

        /// <summary>
        /// Создаёт пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public void CreateUser(string name)
        {
            if (!users.Contains(FindUserForCreate(name)))
            {
                users.Add(new User(name));
                Console.WriteLine($"Пользователь с именем {name} успешно создан.");
                return;
            }

            throw new ArgumentException($"Пользователь с именем {name} уже существует.");
        }

        /// <summary>
        /// Находит заданного пользователя по имени.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Пользователь.</returns>
        private User FindUserForCreate(string userName)
        {
            User user;

            foreach (var item in users)
            {
                if (item.Name == userName)
                {
                    return user = item;
                }
            }

            return user = new User("~");
        }

        /// <summary>
        /// Удаляет пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public void RemoveUser(string userName)
        {
            foreach (var item in users)
            {
                if (item.Name == userName)
                {
                    users.Remove(item);
                    Console.WriteLine($"Пользователь с именем {item.Name} был удалён.");
                    return;
                }
            }

            throw new ArgumentException($"Пользователь с именем {userName} не был найден. Удаление не произошло.");
        }

        /// <summary>
        /// Выводит информацию о пользоватлеях на консоль.
        /// </summary>
        public void ShowUsers()
        {
            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Сохраняет состояние приложения в файл.
        /// </summary>
        /// <param name="taskManager">Объект класса TaskManager.</param>
        public void SaveStateToFile(TaskManager taskManager)
        {
            try
            {
                DeleteDirectory(saveDirectoryName);
                Directory.CreateDirectory(saveDirectoryName);
                File.WriteAllText(Path.Combine(saveDirectoryName, usersSaveFileName), JsonConvert.SerializeObject(users));
                File.WriteAllText(Path.Combine(saveDirectoryName, projectsSaveFileName), JsonConvert.SerializeObject(projects));
                for (int i = 0; i < projects.Count; i++)
                {
                    var currentDirectory = Path.Combine(saveDirectoryName, projects[i].Name);
                    if (projects[i].tasks.Count > 0)
                    {
                        Directory.CreateDirectory(currentDirectory);
                        foreach (var task in projects[i].tasks)
                        {
                            switch (task)
                            {
                                case Epic e:
                                    var epicsFilePath = Path.Combine(currentDirectory, epicsSaveFileName);
                                    using (StreamWriter esw = new StreamWriter(epicsFilePath, true))
                                    {
                                        esw.WriteLine(JsonConvert.SerializeObject(task));
                                        if (e.Tasks.Count == 0)
                                        {
                                            continue;
                                        }
                                        var epicCurrentDirectory = Path.Combine(currentDirectory, e.Name);
                                        Directory.CreateDirectory(epicCurrentDirectory);
                                        foreach (var epicTask in e.Tasks)
                                        {
                                            switch (epicTask)
                                            {
                                                case Story s:
                                                    var epicStoriesFilePath = Path.Combine(epicCurrentDirectory, storiesSaveFileName);
                                                    using (StreamWriter ssw = new StreamWriter(epicStoriesFilePath, true))
                                                    {
                                                        ssw.WriteLine(JsonConvert.SerializeObject(epicTask));
                                                    }
                                                    break;

                                                case Task t:
                                                    var epicTasksNamePath = Path.Combine(epicCurrentDirectory, tasksSaveFileName);
                                                    using (StreamWriter tsw = new StreamWriter(epicTasksNamePath, true))
                                                    {
                                                        tsw.WriteLine(JsonConvert.SerializeObject(epicTask));
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case Bug b:
                                    var bugsFilePath = Path.Combine(currentDirectory, bugsSaveFileName);
                                    using (StreamWriter bsw = new StreamWriter(bugsFilePath, true))
                                    {
                                        bsw.WriteLine(JsonConvert.SerializeObject(task));
                                    }
                                    break;

                                case Story s:
                                    var storiesFilePath = Path.Combine(currentDirectory, storiesSaveFileName);
                                    using (StreamWriter ssw = new StreamWriter(storiesFilePath, true))
                                    {
                                        ssw.WriteLine(JsonConvert.SerializeObject(task));
                                    }
                                    break;

                                case Task t:
                                    var tasksNamePath = Path.Combine(currentDirectory, tasksSaveFileName);
                                    using (StreamWriter tsw = new StreamWriter(tasksNamePath, true))
                                    {
                                        tsw.WriteLine(JsonConvert.SerializeObject(task));
                                    }
                                    break;
                            }

                        }
                    }

                    //using StreamWriter projectsfile = new StreamWriter($"{i}/{projectsSaveFileName}", false);
                    //projectsfile.writeline(user.name);

                }

                Console.WriteLine("Сохранение выполнено успешно.");
            }
            catch (Exception ex)
            {

            }
        }

        public static void DeleteDirectory(string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                return;
            }
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void RestoreUsersFromFile()
        {
            try
            {
                var usersPath = Path.Combine(saveDirectoryName, usersSaveFileName);
                if (!File.Exists(usersPath))
                {
                    return;
                }
                this.users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(usersPath));
                return;
            }
            catch (Exception ex)
            {
                this.users = new List<User>();
            }
        }

        public void RestoreProjectsFromFile()
        {
            try
            {
                var projectPath = Path.Combine(saveDirectoryName, projectsSaveFileName);
                if (!File.Exists(projectPath))
                {
                    return;
                }
                this.projects = JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText(projectPath));
                foreach (var project in this.projects)
                {
                    var currentDirectory = Path.Combine(saveDirectoryName, project.Name);
                    if (Directory.Exists(currentDirectory))
                    {
                        var epicsFilePath = Path.Combine(currentDirectory, epicsSaveFileName);
                        var epiks = GetListTasksFromFile<Epic>(epicsFilePath);
                        foreach (var epic in epiks)
                        {
                            project.AddTask(epic);
                            var epicTasksPath = Path.Combine(currentDirectory, epic.Name);
                            if (Directory.Exists(epicTasksPath))
                            {
                                var epicStoriesFilePath = Path.Combine(epicTasksPath, storiesSaveFileName);
                                var epicStories = GetListTasksFromFile<Story>(epicStoriesFilePath);
                                foreach (var story in epicStories)
                                {
                                    ReattachUserAfterRestore(story);
                                    epic.AddTask(story);
                                }

                                var epicTasksNamePath = Path.Combine(epicTasksPath, tasksSaveFileName);
                                var epicTasks = GetListTasksFromFile<Task>(epicTasksNamePath);
                                foreach (var task in epicTasks)
                                {
                                    ReattachUserAfterRestore(task);
                                    epic.AddTask(task);
                                }
                            }
                        }

                        var storiesFilePath = Path.Combine(currentDirectory, storiesSaveFileName);
                        var stories = GetListTasksFromFile<Story>(storiesFilePath);
                        foreach (var story in stories)
                        {
                            ReattachUserAfterRestore(story);
                            project.AddTask(story);
                        }

                        var tasksNamePath = Path.Combine(currentDirectory, tasksSaveFileName);
                        var tasks = GetListTasksFromFile<Task>(tasksNamePath);
                        foreach (var task in tasks)
                        {
                            ReattachUserAfterRestore(task);
                            project.AddTask(task);
                        }

                        var bugsFilePath = Path.Combine(currentDirectory, bugsSaveFileName);
                        var bugs = GetListTasksFromFile<Bug>(bugsFilePath);
                        foreach (var bug in bugs)
                        {
                            ReattachUserAfterRestore(bug);
                            project.AddTask(bug);
                        }
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                this.projects = new List<Project>();
            }
        }

        private List<T> GetListTasksFromFile<T>(string path)
        {
            var tasks = new List<T>();
            if (File.Exists(path))
            {
                using (StreamReader ssw = new StreamReader(path))
                {
                    while (true)
                    {
                        var newLine = ssw.ReadLine();
                        if (newLine is null)
                        {
                            break;
                        }
                        if (string.IsNullOrWhiteSpace(newLine))
                        {
                            continue;
                        }
                        tasks.Add(JsonConvert.DeserializeObject<T>(newLine));
                    };
                }
            }
            return tasks;
        }

        private void ReattachUserAfterRestore(BaseTask task)
        {
            if (task is IAssignable)
            {
                var assignableTask = task as IAssignable;
                var savedTaskUsers = assignableTask.Users;
                for (int i = 0; i < savedTaskUsers.Count; i++)
                {
                    var userFromManagerList = this.users.FirstOrDefault(x => x.Name == savedTaskUsers[i].Name);
                    if (!(userFromManagerList is null))
                    {
                        assignableTask.UnassignUsers(savedTaskUsers[i]);
                        assignableTask.AssignUsers(userFromManagerList);
                    }
                }
            }
        }
    }
}

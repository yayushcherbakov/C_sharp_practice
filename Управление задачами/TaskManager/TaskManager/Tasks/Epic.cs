using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TaskManager.Tasks.Base;
using TaskManager.Tasks.Interfaces;


namespace TaskManager.Tasks
{
    public class Epic : BaseTask
    {

        [JsonIgnore]

        /// <summary>
        /// Список задач.
        /// </summary>
        private readonly List<BaseTask> tasks;

        /// <summary>
        /// Конструктор Epic.
        /// </summary>
        /// <param name="name">Название задачи.</param>
        public Epic(string name) : base(name)
        {
            this.tasks = new List<BaseTask>();
        }

        [JsonIgnore]

        /// <summary>
        /// Свойство списка тасков.
        /// </summary>
        public List<BaseTask> Tasks
        {
            get
            {
                var listToRead = new List<BaseTask>();

                foreach (var task in tasks)
                {
                    listToRead.Add(task);
                }

                return listToRead;
            }
        }

        /// <summary>
        /// Добавляет задачу в список задач.
        /// </summary>
        /// <param name="task">Добавляемая задача.</param>
        public void AddTask(BaseTask task)
        {
            switch (task)
            {
                case Epic e:
                    throw new ArgumentException("Задача Epic не может включать в себя подзадачу Epic.");

                case Bug b:
                    throw new ArgumentException("Задача Epic не может включать в себя подзадачу Bug.");

                case Story s:
                    tasks.Add(task);
                    return;

                case Task t:
                    tasks.Add(task);
                    return;

                default:
                    throw new ArgumentException("Некорректный тип задачи.");
            }
        }

        /// <summary>
        /// Удаляет задачу из списка задач.
        /// </summary>
        /// <param name="task">Удаляемая задача.</param>
        public void RemoveTask(BaseTask task)
        {
            if (!tasks.Contains(task))
            {
                throw new ArgumentException("Данной задачи нет в списке подзадач Epic. Удаление не произошло.");
            }

            tasks.Remove(task);

            Console.WriteLine($"Удаление подзадачи {task.Name} из задачи типа Epic c названием {this.Name} произошло успешно.");
        }

        /// <summary>
        /// Переопределяет метод ToString().
        /// </summary>
        /// <returns>Информация о задаче Epic.</returns>
        public override string ToString()
        {
            string output;

            output = $"Тип задачи: Epic, название задачи: {this.Name}, дата создания задачи: {this.CreationDate}, статус задачи: {this.Status}";

            switch (tasks.Count)
            {
                case 0:
                    output += ", нет прикреплённых подзадач.";
                    return output;

                case 1:
                    output += $", прикреплённая подзадача: {tasks[0].Name}";
                    var currentTask = ((IAssignable)tasks[0]);

                    switch (currentTask.Users.Count)
                    {
                        case 0:
                            output += " без назначенных пользователей.";
                            break;

                        case 1:
                            output += $" с назначенным пользователем по имени {currentTask.Users[0].Name}.";
                            break;

                        default:
                            output += $" с назначенными пользователями по имени";

                            foreach (var user in currentTask.Users)
                            {
                                output += $" {user.Name},";
                            }

                            output += $"\b.";
                            break;
                    }
                    return output;

                default:
                    foreach (var task in tasks)
                    {
                        output += $", прикреплённая подзадача: {task.Name}";
                        var currentTask1 = ((IAssignable)task);

                        switch (currentTask1.Users.Count)
                        {
                            case 0:
                                output += " без назначенных пользователей;";
                                break;

                            case 1:
                                output += $" с назначенным пользователем по имени {currentTask1.Users[0].Name};";
                                break;

                            default:
                                output += $" с назначенными пользователями по имени";

                                foreach (var user in currentTask1.Users)
                                {
                                    output += $" {user.Name},";
                                }

                                output += $"\b;";
                                break;
                        }
                    }

                    return output += "\b."; ;
            }
        }
    }
}

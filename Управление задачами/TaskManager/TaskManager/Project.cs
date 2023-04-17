using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TaskManager.Tasks.Base;

namespace TaskManager
{
    public class Project
    {
        /// <summary>
        /// Название проекта.
        /// </summary>
        private string name;

        /// <summary>
        /// Максимальное количество задач в проекте.
        /// </summary>
        private int maxTasksCount;

        [JsonIgnore]

        /// <summary>
        /// Список задач в проекте.
        /// </summary>
        public List<BaseTask> tasks;

        /// <summary>
        /// Свойство имени проекта.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Некорректные данные названия проекта.");
                }

                name = value;
            }
        }

        /// <summary>
        /// Свойство максимального количества задач в проекте.
        /// </summary>
        public int MaxTasksCount
        {
            get
            {
                return maxTasksCount;
            }

            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Некорректные данные максимального количества задач.");
                }

                maxTasksCount = value;
            }
        }

        /// <summary>
        /// Конструктор Project.
        /// </summary>
        /// <param name="name">Название проекта.</param>
        /// <param name="maxTasksCount">Максимальное количество задач в проекте.</param>
        public Project(string name, int maxTasksCount)
        {
            this.Name = name;
            this.MaxTasksCount = maxTasksCount;
            this.tasks = new List<BaseTask>();
        }

        /// <summary>
        /// Добавляет задачу в список задач проекта.
        /// </summary>
        /// <param name="task">Добавляемая задача.</param>
        public void AddTask(BaseTask task)
        {
            if (tasks.Count == maxTasksCount)
            {
                throw new ApplicationException("Достигнуто максимальное количество задач. Невозможно добавить новую.");
            }

            tasks.Add(task);
        }

        /// <summary>
        /// Удаляет задачу из списка задач проекта.
        /// </summary>
        /// <param name="task">Удаляемая задача.</param>
        public void RemoveTask(BaseTask task)
        {
            if (!tasks.Contains(task))
            {
                throw new ArgumentException("Задача не была найдена. Удаление не произошло.");
            }

            tasks.Remove(task);
        }

        /// <summary>
        /// Переопределяет метод ToString().
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Название проекта: {this.Name}, количество связанных с ним задач: {this.tasks.Count}.";
        }
    }
}

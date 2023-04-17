using System;
using TaskManager.Tasks.Enums;

namespace TaskManager.Tasks.Base
{
    public abstract class BaseTask
    {
        /// <summary>
        /// Название задачи.
        /// </summary>
        private string name;

        /// <summary>
        /// Дата создания задачи.
        /// </summary>
        private readonly DateTime creationDate;

        /// <summary>
        /// Статус задачи.
        /// </summary>
        private TaskStatus status;

        /// <summary>
        /// Свойство статуса задачи.
        /// </summary>
        public TaskStatus Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        /// <summary>
        /// Свойство даты создания задачи.
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }
        }

        /// <summary>
        /// Свойство названия задачи.
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
                    throw new ArgumentException("Некорректные данные названия задачи.");
                }

                name = value;
            }
        }

        /// <summary>
        /// Конструктор класса BaseTask.
        /// </summary>
        /// <param name="name">Название задачи.</param>
        public BaseTask(string name)
        {
            this.Name = name;
            this.creationDate = DateTime.Now;
            this.Status = TaskStatus.Opened;
        }
    }
}

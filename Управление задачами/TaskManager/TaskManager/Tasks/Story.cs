using System;
using System.Collections.Generic;
using TaskManager.Tasks.Base;
using TaskManager.Tasks.Interfaces;

namespace TaskManager.Tasks
{
    public class Story : BaseTask, IAssignable
    {
        /// <summary>
        /// Список пользователей.
        /// </summary>
        private readonly List<User> users = new List<User>();

        /// <summary>
        /// Конструктор Story.
        /// </summary>
        /// <param name="name">Название задачи.</param>
        public Story(string name) : base(name)
        {

        }

        /// <summary>
        /// Свойство списка пользователей.
        /// </summary>
        public List<User> Users
        {
            get
            {
                var listToRead = new List<User>();

                foreach (var user in users)
                {
                    listToRead.Add(user);
                }

                return listToRead;
            }
        }

        /// <summary>
        /// Прикрепляет пользователя к задаче.
        /// </summary>
        /// <param name="user">Прикрепляемый пользователь.</param>
        public void AssignUsers(User user)
        {
            if (users.Contains(user))
            {
                throw new ApplicationException("Данный исполнитель уже выполняет эту задачу. Невозможно назначить его повторно.");
            }

            users.Add(user);
        }

        /// <summary>
        /// Открепляет пользователя от задачи.
        /// </summary>
        /// <param name="user">Открепляемый пользователь.</param>
        public void UnassignUsers(User user)
        {
            if (!users.Contains(user))
            {
                throw new ArgumentException("Исполнитель не был найден. Невозможно его отстранить.");
            }

            users.Remove(user);
        }

        /// <summary>
        /// Переопределяет метод ToString().
        /// </summary>
        /// <returns>Информация о задаче Story.</returns>
        public override string ToString()
        {
            string output;

            output = $"Тип задачи: Story, название задачи: {this.Name}, дата создания задачи: {this.CreationDate}, статус задачи: {this.Status}";

            switch (users.Count)
            {
                case 0:
                    output += ", нет прикреплённых исполнителей.";
                    return output;

                case 1:
                    output += $", прикреплённый исполнитель: {users[0].Name}.";
                    return output;

                default:
                    output += ", прикреплённые исполнители:";

                    foreach (var user in users)
                    {
                        output += $" {user.Name},";
                    }

                    output += "\b.";
                    return output;
            }
        }
    }
}

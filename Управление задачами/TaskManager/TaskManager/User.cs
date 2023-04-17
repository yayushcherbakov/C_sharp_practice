using System;

namespace TaskManager
{
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        private string name;

        /// <summary>
        /// Свойство имени пользователя.
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
                    throw new ArgumentException("Некорректное имя пользователя.");
                }

                name = value;
            }
        }

        /// <summary>
        /// Конструктор класса User.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public User(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Переопределяет метод ToString().
        /// </summary>
        /// <returns>Информация об имени пользователя.</returns>
        public override string ToString()
        {
            return $"Пользователь с именем {name}.";
        }
    }
}

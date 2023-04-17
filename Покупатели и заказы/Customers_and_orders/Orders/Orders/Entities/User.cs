using Orders.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Entities
{
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хеш пароля.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Статус пользователя.
        /// </summary>
        public UserRole Role { get; set; }  

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string Middlename { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Номер телефона пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адрес пользователя.
        /// </summary>
        public string Address { get; set; }
    }
}

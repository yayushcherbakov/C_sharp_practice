using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; }
    }
}

using Newtonsoft.Json;
using Orders.Entities;
using Orders.Enums;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders.Managers
{
    public class UserManager
    {
        private static readonly string userStorageFileName = "users.json";

        /// <summary>
        /// Регистрирует пользователя.
        /// </summary>
        /// <param name="model">Данные пользователя.</param>
        public void RegisterUser(RegisterModel model)
        {
            var guid = Guid.NewGuid().ToString();
            var newUser = new User()
            {
                Id = guid,
                Email = model.Email,
                PasswordHash = GenerateHash(model.Password, guid),
                Firstname = model.Firstname,
                Middlename = model.Middlename,
                Lastname = model.Lastname,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role
            };

            CreateUser(newUser);
        }

        /// <summary>
        /// Генерирует хеш.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <param name="guid"></param>
        /// <returns>Хеш.</returns>
        private string GenerateHash(string password, string guid)
        {
            var hash = string.Empty;
            var modifiedPassword = $"{password}{guid}";
            var textBytes = Encoding.UTF8.GetBytes(modifiedPassword);
            var keyBytes = Encoding.UTF8.GetBytes(guid);
            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(textBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            return hash;
        }

        /// <summary>
        /// Залогинивание пользователя.
        /// </summary>
        /// <param name="model">Данные логина.</param>
        /// <returns>Пользователь.</returns>
        public User LoginUser(LoginModel model)
        {
            var users = GetUsersFromFile();
            var currentTocheck = users.FirstOrDefault(user => user.Email.ToLower() == model.Login.ToLower());
            if (currentTocheck is null)
            {
                throw new ApplicationException("User does not exist");
            }
            var currentPasswordHash = GenerateHash(model.Password, currentTocheck.Id);
            if (currentTocheck.PasswordHash != currentPasswordHash)
            {
                throw new ApplicationException("Incorrect password");
            }
            return currentTocheck;
        }

        /// <summary>
        /// Получает пользователей из файла.
        /// </summary>
        /// <returns>Пользователи.</returns>
        private List<User> GetUsersFromFile()
        {
            var userList = new List<User>();
            if (File.Exists(userStorageFileName))
            {
                try
                {
                    var content = File.ReadAllText(userStorageFileName, Encoding.UTF8);
                    userList = JsonConvert.DeserializeObject<List<User>>(content);
                }
                catch
                {
                    throw new ApplicationException("Can't get users");
                }
            }
            return userList;
        }

        /// <summary>
        /// Получает пользователя по айди.
        /// </summary>
        /// <param name="userId">Айди пользователя.</param>
        /// <returns>Пользователь.</returns>
        public User GetUserById(string userId)
        {
            var userList = GetUsersFromFile();
            var user = userList.FirstOrDefault(x => x.Id == userId);
            if (user is null)
            {
                throw new ApplicationException("Can't get user information");
            }
            return user;
        }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="newUser">Пользователь.</param>
        private void CreateUser(User newUser)
        {

            var users = GetUsersFromFile();
            if (users.Any(user => user.Email.ToLower() == newUser.Email.ToLower()))
            {
                throw new ApplicationException("User with this email alredy exists");
            }

            try
            {
                users.Add(newUser);
                string output = JsonConvert.SerializeObject(users);
                File.WriteAllText(userStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save users");
            }
        }


        /// <summary>
        /// Получает список всех покупателей.
        /// </summary>
        /// <returns>Покупатели.</returns>
        public List<User> GetAllBuyers()
        {
            return GetUsersFromFile().Where(u => u.Role == UserRole.Buyer).ToList();
        }
    }
}

using Messager.Entities;
using Messager.Exceptions;
using Messager.Managers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messager.Managers
{
    public class UserManager : IUserManager
    {
        /// <summary>
        /// User storage file name.
        /// </summary>
        private static readonly string userStorageFileName = "users.json";

        /// <summary>
        /// Gets users from file.
        /// </summary>
        public List<User> Users
        {
            get
            {
                return GetUsersFromFile();
            }
        }

        /// <summary>
        /// Gets users from file.
        /// </summary>
        /// <returns>Users list.</returns>
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
        /// Gets saved letters.
        /// </summary>
        /// <param name="users">Users list.</param>
        public void InitUsers(List<User> users)
        {
            try
            {
                string output = JsonConvert.SerializeObject(users.OrderBy(x => x.Email).ToList());
                File.WriteAllText(userStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save users");
            }
        }

        /// <summary>
        /// Deletes users.
        /// </summary>
        public void DeleteUsers()
        {
            try
            {
                string output = JsonConvert.SerializeObject(new List<User>());
                File.WriteAllText(userStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save users");
            }
        }

        /// <summary>
        /// Gets users by ID.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>User.</returns>
        public User GetUserById(string userId)
        {
            var user = Users.FirstOrDefault(x => x.Email.ToLowerInvariant() == userId.ToLowerInvariant());
            if (user is null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }

        /// <summary>
        /// Check to user exist.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>Check result.</returns>
        public bool IsUserExsits(string userId)
        {
            return Users.Any(x => x.Email.ToLowerInvariant() == userId.ToLowerInvariant());
        }

        /// <summary>
        /// Saves user.
        /// </summary>
        /// <param name="user">User.</param>
        public void SaveUser(User user)
        {
            var users = GetUsersFromFile();
            users.Add(user);
            try
            {
                string output = JsonConvert.SerializeObject(users.OrderBy(x => x.Email).ToList());
                File.WriteAllText(userStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save users");
            }
        }
    }
}

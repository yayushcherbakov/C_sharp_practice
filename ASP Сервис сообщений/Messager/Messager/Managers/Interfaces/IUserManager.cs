using Messager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messager.Managers.Interfaces
{
    public interface IUserManager
    {
        List<User> Users { get; }

        // Deletes users.
        void DeleteUsers();

        // Gets user bu ID.
        User GetUserById(string userId);

        // Inits users.
        void InitUsers(List<User> users);

        // Check to user exsists.
        bool IsUserExsits(string userId);

        // Saves user.
        void SaveUser(User user);
    }
}

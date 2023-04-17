using Messager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messager.Service.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// Creates random users.
        /// </summary>
        /// <param name="count">Users count.</param>
        void CreateRandomUsers(int count);

        /// <summary>
        /// Deletes all users and messages.
        /// </summary>
        void DeleteUsersAndMessages();

        /// <summary>
        /// Registers user.
        /// </summary>
        /// <param name="user">User.</param>
        void RegisterUser(User user);

        /// <summary>
        /// Gets user info.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>User info.</returns>
        User GetUserInfo(string userId);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="limit">Limit.</param>
        /// <param name="offset">Offset.</param>
        /// <returns>Users.</returns>
        List<User> GetAllUsers(int limit, int offset);

        /// <summary>
        /// Gets messages.
        /// </summary>
        /// <param name="sendlerId">Senser ID.</param>
        /// <param name="receiverId">Receiver ID.</param>
        /// <returns>Messages.</returns>
        List<Letter> GetMessages(string sendlerId, string receiverId);

        // Gets messages by sender.
        List<Letter> GetMessagesBySender(string sendlerId);

        // Gets messages by receiver.
        List<Letter> GetMessagesByReceiver(string receiverId);

        // Sends message.
        void SendMessage(Letter message);
    }
}

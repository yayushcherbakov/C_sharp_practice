using Messager.Entities;
using Messager.Managers;
using Messager.Managers.Interfaces;
using Messager.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messager.Service
{
    public class MessageService: IMessageService
    {
        private readonly IUserManager _userManager;
        private readonly ILetterManager _letterManager;
        public MessageService(IUserManager userManager, ILetterManager letterManager)
        {
            _userManager = userManager;
            _letterManager = letterManager;
        }

        // Creats random users.
        public void CreateRandomUsers(int userCount)
        {
            var users = _userManager.Users;
            if (users.Count > 0)
            {
                throw new ApplicationException("Users are already initialized");
            }

            if (_letterManager.Letters.Count > 0)
            {
                throw new ApplicationException("Messages are already initialized");
            }

            var rnd = new Random();
            for (int i = 0; i < userCount; i++)
            {
                var newUser = new User();
                do
                {
                    newUser.UserName = $"email{rnd.Next(1, int.MaxValue)}";
                    newUser.Email = $"{newUser.UserName}@email.com";
                } while (users.Any(x => x.Email == newUser.Email));

                users.Add(newUser);
            }
            _userManager.InitUsers(users);

            var letters = new List<Letter>();
            foreach (var sender in users)
            {
                foreach (var receiver in users.Where(u=>u!= sender).ToList())
                {
                    var messageCount = rnd.Next(1, 5);
                    for(int i = 0; i < messageCount; i++)
                    {
                        var letter = new Letter()
                        {
                            SenderId = sender.Email,
                            ReceiverId = receiver.Email,
                            Subject = "Random generation",
                            Message = $"message{rnd.Next(1, int.MaxValue)}"
                        };
                        letters.Add(letter);
                    }
                    
                }
            }

            _letterManager.InitLetters(letters);
        }

        // Deletes all users and messages.
        public void DeleteUsersAndMessages()
        {
            _userManager.DeleteUsers();
            _letterManager.DeleteMessages();
        }

        // Gets all users.
        public List<User> GetAllUsers(int limit, int offset)
        {
            return _userManager.Users.Skip(offset).Take(limit).ToList();
        }

        // Gets messages.
        public List<Letter> GetMessages(string sendlerId, string receiverId)
        {
            if (!_userManager.IsUserExsits(sendlerId))
            {
                throw new ApplicationException("User with SenderId not exist");

            }
            if (!_userManager.IsUserExsits(receiverId))
            {
                throw new ApplicationException("User with ReceiverId not exist");

            }

            return _letterManager.Letters.Where(
                x => 
                    x.SenderId.ToLowerInvariant() == sendlerId.ToLowerInvariant()
                    &&
                    x.ReceiverId.ToLowerInvariant() == receiverId.ToLowerInvariant()
                ).ToList();
        }

        // Gets messages by receiver.
        public List<Letter> GetMessagesByReceiver(string receiverId)
        {
            if (!_userManager.IsUserExsits(receiverId))
            {
                throw new ApplicationException("User with ReceiverId not exist");

            }
            return _letterManager.Letters.Where(x => x.ReceiverId.ToLowerInvariant() == receiverId.ToLowerInvariant()).ToList();
        }

        // Gets messages by sender.
        public List<Letter> GetMessagesBySender(string sendlerId)
        {
            if (!_userManager.IsUserExsits(sendlerId))
            {
                throw new ApplicationException("User with SenderId not exist");

            }
            return _letterManager.Letters.Where(x => x.SenderId.ToLowerInvariant() == sendlerId.ToLowerInvariant()).ToList();
        }

        // Gets user info.
        public User GetUserInfo(string userId)
        {
            return _userManager.GetUserById(userId);
        }

        // Registers user.
        public void RegisterUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email)|| string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ApplicationException("Invalid user fields");
            }

            if (_userManager.IsUserExsits(user.Email))
            {
                throw new ApplicationException("The user already exists");

            }
            _userManager.SaveUser(user);
        }

        // Sends message.
        public void SendMessage(Letter message)
        {
            if (string.IsNullOrWhiteSpace(message.Subject))
            {
                throw new ApplicationException("Subject can not be null or whitespace");
            }
            if (string.IsNullOrWhiteSpace(message.Message))
            {
                throw new ApplicationException("Message can not be null or whitespace");
            }

            if (!_userManager.IsUserExsits(message.SenderId))
            {
                throw new ApplicationException("User with SenderId not exist");

            }

            if (!_userManager.IsUserExsits(message.ReceiverId))
            {
                throw new ApplicationException("User with ReceiverId not exist");

            }

            _letterManager.SaveMessage(message);
        }
    }
}

using System.Collections.Generic;

namespace TaskManager.Tasks.Interfaces
{
    public interface IAssignable
    {
        /// <summary>
        /// Свойство списка пользователей.
        /// </summary>
        public List<User> Users { get; }

        /// <summary>
        /// Прикрепить пользователя.
        /// </summary>
        /// <param name="user">Прикрепляемый пользователь.</param>
        public void AssignUsers(User user);

        /// <summary>
        /// Открепить пользователя.
        /// </summary>
        /// <param name="user">Открепляемый пользователь.</param>
        public void UnassignUsers(User user);
    }
}

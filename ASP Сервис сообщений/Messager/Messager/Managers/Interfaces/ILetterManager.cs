using Messager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messager.Managers.Interfaces
{
    public interface ILetterManager
    {
        List<Letter> Letters { get; }

        // Delets messages.
        void DeleteMessages();

        // Inits letters.
        void InitLetters(List<Letter> letters);

        // Saves letters.
        void SaveMessage(Letter letter);
    }
}

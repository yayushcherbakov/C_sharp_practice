using Messager.Entities;
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
    public class LetterManager : ILetterManager
    {
        /// <summary>
        /// Letter storage file name.
        /// </summary>
        private static readonly string letterStorageFileName = "Letters.json";

        /// <summary>
        /// Gets letters from file.
        /// </summary>
        public List<Letter> Letters
        {
            get
            {
                return GetLettersFromFile();
            }
        }

        /// <summary>
        /// Gets letters from file.
        /// </summary>
        /// <returns>Letters list.</returns>
        private List<Letter> GetLettersFromFile()
        {
            var letterList = new List<Letter>();
            if (File.Exists(letterStorageFileName))
            {
                try
                {
                    var content = File.ReadAllText(letterStorageFileName, Encoding.UTF8);
                    letterList = JsonConvert.DeserializeObject<List<Letter>>(content);
                }
                catch
                {
                    throw new ApplicationException("Can't get letters");
                }
            }
            return letterList;
        }


        /// <summary>
        /// Gets saved letters to file.
        /// </summary>
        /// <param name="letters">Saved letters.</param>
        public void InitLetters(List<Letter> letters)
        {
            try
            {
                string output = JsonConvert.SerializeObject(letters);
                File.WriteAllText(letterStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save letters");
            }
        }


        /// <summary>
        /// Deletes messages.
        /// </summary>
        public void DeleteMessages()
        {
            try
            {
                string output = JsonConvert.SerializeObject(new List<Letter>());
                File.WriteAllText(letterStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save letters");
            }
        }

        /// <summary>
        /// Saves letter.
        /// </summary>
        /// <param name="letter">Saved letter.</param>
        public void SaveMessage(Letter letter)
        {
            var letters = GetLettersFromFile();
            letters.Add(letter);
            try
            {
                string output = JsonConvert.SerializeObject(letters);
                File.WriteAllText(letterStorageFileName, output, Encoding.UTF8);
            }
            catch
            {
                throw new ApplicationException("Can't save letter");
            }
        }
    }
}

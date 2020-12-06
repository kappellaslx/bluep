using System.Collections.Generic;
using System.IO;

namespace BluePrismConsoleApp
{
    public class EnglishDictionary : IEnglishDictionary
    {
        const string fileName = "words-english.txt";
        private Dictionary<string, string> englishDictionary;
        private object lockObj = new object();

        /// <inheritdoc/>
        public Dictionary<string, string> GetEnglishDictionary()
        {
            if (this.englishDictionary == null)
            {
                this.LoadEnglishDictionary(fileName);
            }

            return this.englishDictionary;
        }

        /// <inheritdoc/>
        public void LoadEnglishDictionary(string filePath)
        {
            lock (this.lockObj)
            {
                Dictionary<string, string> wordsDict = new Dictionary<string, string>();
                IEnumerable<string> words = File.ReadLines(filePath);

                foreach (string word in words)
                {
                    if (!string.IsNullOrEmpty(word) // avoid null or empty words
                        && Constants.WordsRegex.IsMatch(word) // filtering words with 4 letters only
                        && !wordsDict.ContainsKey(word.ToLower())) // avoid repeated words
                    {
                        wordsDict.Add(word.ToLower(), word);
                    }
                }

                this.englishDictionary = wordsDict;
            }
        }
    }
}
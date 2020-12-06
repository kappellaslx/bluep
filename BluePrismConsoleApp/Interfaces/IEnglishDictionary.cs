using System.Collections.Generic;

namespace BluePrismConsoleApp
{
    public interface IEnglishDictionary
    {
        /// <summary>
        /// Get English dictionary
        /// </summary>
        /// <returns>list of words</returns>
        Dictionary<string, string> GetEnglishDictionary();

        /// <summary>
        /// Load English dictionary
        /// </summary>
        /// <param name="filePath">English dictionary file path</param>
        void LoadEnglishDictionary(string filePath);
    }
}

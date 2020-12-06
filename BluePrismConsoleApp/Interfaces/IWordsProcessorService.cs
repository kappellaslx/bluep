using System.Collections.Generic;

namespace BluePrismConsoleApp
{
    public interface IWordsProcessorService
    {
        /// <summary>
        /// Process file words
        /// </summary>
        /// <param name="words">words list</param>
        /// <param name="startWord">start word</param>
        /// <param name="endWord">end word</param>
        /// <returns>list of filtered words</returns>
        List<string> ProcessWords(IEnumerable<string> words, string startWord, string endWord);

        /// <summary>
        /// Filter and sort list of words
        /// </summary>
        /// <param name="words">list of word</param>
        /// <returns>Filtered and sorted list of words</returns>
        IEnumerable<string> FilterAndSortWords(IEnumerable<string> words);

        /// <summary>
        /// Check if the word is valid
        /// </summary>
        /// <param name="currentWord"></param>
        /// <returns></returns>
        bool IsValidWord(string word);

        /// <summary>
        /// Compare words
        /// </summary>
        /// <param name="previousWord">previous word</param>
        /// <param name="currentWord">current word</param>
        /// <returns>only 1 letter changed</returns>
        bool CompareWords(string previousWord, string currentWord);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace BluePrismConsoleApp
{
    public class WordsProcessorService : IWordsProcessorService
    {
        private readonly Dictionary<string, string> englishWords;

        public WordsProcessorService(IEnglishDictionary englishDictionary)
        {
            this.englishWords = englishDictionary.GetEnglishDictionary();
        }

        /// <inheritdoc/>
        public List<string> ProcessWords(IEnumerable<string> words, string startWord, string endWord)
        {
            this.CheckArguments(words, startWord, endWord);

            // filter and sort words
            words = this.FilterAndSortWords(words);

            List<string> result = new List<string>();
            int startIndex = -1, endIndex = -1;
            string currentWord, previousWord = null;
            for (int i = 0; i < words.Count(); i++)
            {
                currentWord = words.ElementAt(i);

                // start word found and not repeated
                if (startIndex == -1 && currentWord.Equals(startWord, StringComparison.OrdinalIgnoreCase))
                {
                    startIndex = i;
                    result.Add(currentWord);

                    previousWord = startWord;

                    continue;
                }

                // start word still not found
                if (startIndex == -1)
                {
                    continue;
                }

                // check if we already found the end word
                if (currentWord.Equals(endWord, StringComparison.OrdinalIgnoreCase))
                {
                    endIndex = i;
                    result.Add(currentWord);

                    break;
                }

                // check if it is a valid word and only 1 letter has changed
                if (this.IsValidWord(currentWord)
                    && this.CompareWords(previousWord, currentWord))
                {
                    result.Add(currentWord);

                    previousWord = currentWord;
                }
            }

            if (startIndex < 0)
            {
                throw new ArgumentException("Start word was not present on the file");
            }

            if (endIndex < 0)
            {
                throw new ArgumentException("End word was not present on the file");
            }

            return result;
        }

        private void CheckArguments(IEnumerable<string> words, string startWord, string endWord)
        {
            if (words == null || !words.Any())
            {
                throw new ArgumentException("No words found on the file");
            }

            if (!this.IsValidWord(startWord))
            {
                throw new ArgumentException("Start word is not a valid English word");
            }

            if (!this.IsValidWord(endWord))
            {
                throw new ArgumentException("End word is not a valid English word");
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> FilterAndSortWords(IEnumerable<string> words)
        {
            return words.Where(w => !string.IsNullOrEmpty(w) && Constants.WordsRegex.IsMatch(w)) // filtering words with 4 letters only
                        .OrderBy(w => w) // order words alphabetically
                        .AsEnumerable();
        }

        /// <inheritdoc/>
        public bool IsValidWord(string word)
        {
            // check if the word is a valid English word
            if (!this.englishWords.ContainsKey(word.ToLower()))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool CompareWords(string previousWord, string currentWord)
        {
            // parameters check
            if (string.IsNullOrEmpty(previousWord) 
                || string.IsNullOrEmpty(currentWord)
                || previousWord.Length != currentWord.Length)
            {
                return false;
            }

            int differences = 0;
            for (int i = 0; i < previousWord.Length; i++)
            {
                if (char.ToLowerInvariant(previousWord[i]) != char.ToLowerInvariant(currentWord[i]))
                {
                    differences++;
                }

                // if we already found more than 1 difference
                if (differences > 1)
                {
                    return false;
                }
            }

            // ignore repeated words
            if (differences == 0)
            {
                return false;
            }

            return true;
        }
    }
}
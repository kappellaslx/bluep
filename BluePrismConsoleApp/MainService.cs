using System;
using System.Collections.Generic;
using System.Linq;

namespace BluePrismConsoleApp
{
    public class MainService : IMainService
    {
        private readonly IWordsProcessorService wordsProcessorService;
        private readonly IFileManager fileManager;

        public MainService(IWordsProcessorService wordsProcessorService, IFileManager fileManager)
        {
            this.wordsProcessorService = wordsProcessorService;
            this.fileManager = fileManager;
        }

        /// <inheritdoc/>
        public void StartService(string dictionaryFile, string startWord, string endWord, string resultFile)
        {
            // read the dictionary file into an ordered list of strings
            IEnumerable<string> words = this.fileManager.ReadFile(dictionaryFile);

            // filter words based on start and end words
            List<string> result = this.wordsProcessorService.ProcessWords(words, startWord, endWord);

            // write the result file
            this.fileManager.WriteFile(resultFile, result);
        }
    }
}
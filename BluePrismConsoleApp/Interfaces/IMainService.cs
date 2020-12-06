using System.Collections.Generic;

namespace BluePrismConsoleApp
{
    public interface IMainService
    {
        /// <summary>
        /// Start Service
        /// </summary>
        /// <param name="dictionaryFile">dictionary file</param>
        /// <param name="startWord">start word</param>
        /// <param name="endWord">end word</param>
        /// <param name="resultFile">result file</param>
        void StartService(string dictionaryFile, string startWord, string endWord, string resultFile);
    }
}

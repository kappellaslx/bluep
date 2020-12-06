using System.Collections.Generic;
using System.Linq;

namespace BluePrismConsoleApp
{
    public interface IFileManager
    {
        /// <summary>
        /// Read file words
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>list of words</returns>
        IEnumerable<string> ReadFile(string fileName);

        /// <summary>
        /// Write result file
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <param name="words">4 letter words list</param>
        void WriteFile(string fileName, List<string> words);
    }
}

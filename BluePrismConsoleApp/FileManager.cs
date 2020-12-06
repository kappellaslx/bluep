using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BluePrismConsoleApp
{
    public class FileManager : IFileManager
    {
        /// <inheritdoc/>
        public IEnumerable<string> ReadFile(string fileName)
        {
            return File.ReadLines(fileName);
        }

        /// <inheritdoc/>
        public void WriteFile(string fileName, List<string> words)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllLines(fileName, words);
        }
    }
}

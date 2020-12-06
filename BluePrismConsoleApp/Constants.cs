using System.Text.RegularExpressions;

namespace BluePrismConsoleApp
{
    public class Constants
    {
        public static Regex WordsRegex = new Regex("^[a-zA-Z]{4}$");
    }
}

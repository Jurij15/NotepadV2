using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplePad.Helpers
{
    public class LanguageHelper
    {
        public static string RemoveSpecialLanguageCharacters(string language)
        {
            // Replace "#" with "sharp"
            string result = Regex.Replace(language, "#", "sharp");

            // Replace "+" with "p"
            result = Regex.Replace(result, @"\+", "p");

            return result;
        }
    }
}

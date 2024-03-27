using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePad
{
    public class LanguageDefinitions
    {
        public static Tuple<string, string, string>[] LanguageDefinitionsList = new Tuple<string, string, string>[]
        {
            Tuple.Create("plaintext", "txt", "Plain Text"),
            Tuple.Create("csharp", "cs", "C#"),
            Tuple.Create("cpp", "cpp", "C++"),
            Tuple.Create("javascript", "js", "JavaScript"),
            Tuple.Create("json", "json", "JSON"),
            Tuple.Create("xml", "xml", "XML"),
            Tuple.Create("html", "html", "HTML"),
        }; // lang name, file ending, display name

        public static string GetLanguageNameFromFileEnding(string FileEnding)
        {
            string res = string.Empty;

            foreach (var tuple in LanguageDefinitionsList)
            {
                if (tuple.Item2 == FileEnding)
                {
                    res = tuple.Item1;
                    break;
                }
            }

            return res;
        }

        public static string GetLanguageDisplayNameFromName(string LanguageName)
        {
            string res = string.Empty;

            foreach (var tuple in LanguageDefinitionsList)
            {
                if (tuple.Item1 == LanguageName)
                {
                    res = tuple.Item3;
                    break;
                }
            }

            return res;
        }

        public static string GetLanguageNameFromDisplayName(string DisplayName)
        {
            string res = string.Empty;

            foreach (var tuple in LanguageDefinitionsList)
            {
                if (tuple.Item3 == DisplayName)
                {
                    res = tuple.Item1;
                    break;
                }
            }

            return res;
        }

        public static List<string> GetAllDisplayNames()
        {
            List<string> langs = new List<string>();

            foreach (var tuple in LanguageDefinitionsList)
            {
                langs.Add(tuple.Item3);
            }

            return langs;
        }

        public static string GetFileEndingFromDisplayName(string DisplayName)
        {
            string res = string.Empty;

            foreach (var tuple in LanguageDefinitionsList)
            {
                if (tuple.Item3 == DisplayName)
                {
                    res = tuple.Item2;
                    break;
                }
            }

            return res;
        }
    }
}

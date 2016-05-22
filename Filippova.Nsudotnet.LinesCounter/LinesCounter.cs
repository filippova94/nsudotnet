using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Filippova.Nsudotnet.LinesCounter
{
    class LinesCounter
    {
        private static string extension;
        private static long lines;
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(Properties.Resources.ExtensionNotFound);
                Console.ReadLine();
                return;
            }
            extension = args[0];
            lines = LinesInDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()));
            Console.WriteLine(Properties.Resources.Total, extension, lines);
            Console.ReadLine();
        }

        static long LinesInDirectory(DirectoryInfo dir)
        {
            long lines = 0;
            try
            {
                var dirs = dir.GetDirectories();
                lines += dirs.Sum(directory => LinesInDirectory(directory));
                var files = dir.GetFiles(string.Concat("*", extension));
                lines += files.Sum(file => LinesInFile(file));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            return lines;
        }

        static long LinesInFile(FileInfo file)
        {
            var regex = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            long lines = 0;
            using (TextReader reader = new StreamReader(file.OpenRead()))
            {
                bool isComment = false;
                string curString;
                while ((curString = reader.ReadLine()) != null)
                {
                    if (isComment)
                        curString = string.Concat("/*", curString);
                    curString = Regex.Replace(curString, regex, String.Empty);
                    if (curString.Contains("/*"))
                    {
                        isComment = true;
                        curString = string.Concat(curString, "*/");
                        curString = Regex.Replace(curString, regex, String.Empty);
                    }
                    else
                        isComment = false;
                    if (!string.IsNullOrWhiteSpace(curString))
                        lines++;
                }
            }
            return lines;
        }
    }
}  
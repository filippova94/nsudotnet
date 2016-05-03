using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Filippova.Nsudotnet.LinesCounter
{
    class LinesCounter
    {
        private static string _extension;

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Программа принимает параметром командной строки тип учитываемых файлов. Пример: .txt");
                Console.ReadLine();
                return;
            }
            _extension = args[0];
            var linesCount = LinesCountInDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()));
            Console.WriteLine(string.Concat("Суммарное количество осмысленных строк во всех учтенных файлах формата ", _extension, " в текущей директории и во всех вложенных директориях : ", linesCount));
            Console.ReadLine();
        }

        static long LinesCountInDirectory(DirectoryInfo dir)
        {
            long linesCount = 0;
            try
            {
                var directories = dir.GetDirectories();
                linesCount += directories.Sum(directory => LinesCountInDirectory(directory));
                var files = dir.GetFiles(string.Concat("*", _extension));
                linesCount += files.Sum(file => LinesCountInFile(file));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return linesCount;
        }

        static long LinesCountInFile(FileInfo file)
        {
            var regex = new Regex("\\/\\*[^\\/]*[^\\*]*\\*\\/|\\/\\/[^\n]*");
            long linesCount = 0;
            using (TextReader reader = new StreamReader(file.OpenRead()))
            {
                string currentString;
                bool isComment = false;
                while ((currentString = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(currentString))
                        continue;
                    if (isComment)
                        currentString = string.Concat("/*", currentString);
                    currentString = regex.Replace(currentString, "");
                    if (currentString.Contains("/*"))
                    {
                        isComment = true;
                        currentString = string.Concat(currentString, "*/");
                        currentString = regex.Replace(currentString, "");
                    }
                    else
                        isComment = false;
                    if (!string.IsNullOrWhiteSpace(currentString))
                        linesCount++;
                }
            }
            return linesCount;
        }
    }
}

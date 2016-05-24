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
            }
            return lines;
        }

        static long LinesInFile(FileInfo file)
        {
            long lines = 0;
            using (TextReader reader = new StreamReader(file.OpenRead()))
            {
                bool isComment = false;
                int commentStart = 0;
                string curString;
                while ((curString = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < curString.Length - 1; i++)
                    {
                        if (curString[i].Equals('/'))
                        {
                            if (curString[i + 1].Equals('*')){
                                isComment = true;
                                commentStart = i;
                            }
                            else
                                if (curString[i + 1].Equals('/'))
                                {
                                    curString = curString.Remove(i, curString.Length - i);
                                    break;
                                }
                        }
                        if (isComment)
                        {
                            if (!curString.Contains("*/"))
                            {
                                curString = curString.Remove(i, curString.Length - i);
                                commentStart = 0;
                                break;
                            }
                            if(curString[i].Equals('*') && curString[i+1].Equals('/'))
                            {
                                curString = curString.Remove(commentStart, i+2 - commentStart);
                                isComment = false;
                                break;
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(curString))
                        lines++;
                }
            }
            return lines;
        }
    }
}  
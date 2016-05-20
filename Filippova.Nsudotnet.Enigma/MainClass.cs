using System;

namespace Filippova.Nsudotnet.Enigma
{
    class MainClass
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Аргументы программы не введены!");
                Console.ReadLine();
                return;
            }
            try
            {
                switch (args[0])
                {
                    case "encrypt":
                        if (args.Length < 4)
                        {
                            Console.WriteLine("Не введены все необходимые 4 аргумента, т.е. имя входного файла, название типа шифрования и имя выходного файла");
                            Console.ReadLine();
                            return;
                        }
                        var encryptor = new Encryptor();
                        encryptor.Encryption(args[1], args[2], args[3]);
                        break;
                    case "decrypt":
                        if (args.Length < 5)
                        {
                            Console.WriteLine("Не введены все необходимые 5 аргументов, т.е. имя входного файла, название типа шифрования, имя файла-ключа и имя выходного файла");
                            Console.ReadLine();
                            return;
                        }
                        var decryptor = new Decryptor();
                        decryptor.Decryption(args[1], args[2], args[3], args[4]);
                        break;
                    default:
                        Console.WriteLine("Первым аргументом должна быть строка: для шифрования encrypt или для расшифровки decrypt");
                        Console.ReadLine();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}

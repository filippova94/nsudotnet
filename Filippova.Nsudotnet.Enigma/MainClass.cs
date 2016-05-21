using System;

namespace Filippova.Nsudotnet.Enigma
{

    class MainClass
    {
        public const string Unknown = "Неизвестный алгоритм шифрования. Должен быть указан один из вариантов - aes, des, rc2, rijndael";
        private const string Encrypt = "encrypt", Decrypt = "decrypt", 
            FirstArg = "Первым аргументом должна быть строка: для шифрования {0} или для расшифровки {1}",
            NullArg = "Аргументы программы не введены!";
        private const string AllArgsForEncryption = "Не введены все необходимые 4 аргумента, т.е. имя входного файла, название типа шифрования и имя выходного файла",
            AllArgsForDecryption = "Не введены все необходимые 5 аргументов, т.е. имя входного файла, название типа шифрования, имя файла-ключа и имя выходного файла";
       
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(NullArg);
                Console.ReadLine();
                return;
            }
            try
            {
                switch (args[0])
                {
                    case Encrypt:
                        if (args.Length < 4)
                        {
                            Console.WriteLine(AllArgsForEncryption);
                            Console.ReadLine();
                            return;
                        }
                        var encryptor = new Encryptor();
                        encryptor.Encryption(args[1], args[2], args[3]);
                        break;
                    case Decrypt:
                        if (args.Length < 5)
                        {
                            Console.WriteLine(AllArgsForDecryption);
                            Console.ReadLine();
                            return;
                        }
                        var decryptor = new Decryptor();
                        decryptor.Decryption(args[1], args[2], args[3], args[4]);
                        break;
                    default:
                        Console.WriteLine(FirstArg, Encrypt, Decrypt);
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

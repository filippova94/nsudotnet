using System;

namespace Filippova.Nsudotnet.Enigma
{

    class MainClass
    {
        private const string Encrypt = "encrypt", Decrypt = "decrypt";
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(Properties.Resources.NullArg);
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
                            Console.WriteLine(Properties.Resources.AllArgsForEncryption);
                            Console.ReadLine();
                            return;
                        }
                        var encryptor = new Encryptor();
                        encryptor.Encryption(args[1], args[2], args[3]);
                        break;
                    case Decrypt:
                        if (args.Length < 5)
                        {
                            Console.WriteLine(Properties.Resources.AllArgsForDecryption);
                            Console.ReadLine();
                            return;
                        }
                        var decryptor = new Decryptor();
                        decryptor.Decryption(args[1], args[2], args[3], args[4]);
                        break;
                    default:
                        Console.WriteLine(Properties.Resources.FirstArg, Encrypt, Decrypt);
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

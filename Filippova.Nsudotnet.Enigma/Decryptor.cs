using System;
using System.Security.Cryptography;
using System.IO;

namespace Filippova.Nsudotnet.Enigma
{
    class Decryptor
    {
        private SymmetricAlgorithm algorithm;

        public void Decryption(string inputFile, string algorithmName, string keyFile, string outputFile)
        {
            switch (algorithmName)
            {
                case "aes":
                    algorithm = new AesCryptoServiceProvider();
                    break;
                case "des":
                    algorithm = new DESCryptoServiceProvider();
                    break;
                case "rc2":
                    algorithm = new RC2CryptoServiceProvider();
                    break;
                case "rijndael":
                    algorithm = new RijndaelManaged();
                    break;
                default:
                    throw new Exception("Неизвестный алгоритм шифрования. Должен быть указан один из вариантов - aes, des, rc2, rijndael");
            }

            byte[] key, IV;

            using (var inFile = new FileStream(keyFile, FileMode.Open))
            {
                using (var writer = new StreamReader(inFile))
                {
                    key = Convert.FromBase64String(writer.ReadLine());
                    IV = Convert.FromBase64String(writer.ReadLine());
                }
            }
            using (FileStream inFile = new FileStream(inputFile, FileMode.Open), outFile = new FileStream(outputFile, FileMode.Create))
            {
                    using (var crypto = new CryptoStream(inFile, algorithm.CreateDecryptor(key, IV), CryptoStreamMode.Read))
                    {
                        crypto.CopyTo(outFile);
                    }
            }
        }
    }
}

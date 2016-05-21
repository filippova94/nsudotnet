using System;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace Filippova.Nsudotnet.Enigma
{
    class Encryptor
    {
        private SymmetricAlgorithm algorithm;
        private const string FileEnd = ".key.txt";

        public void Encryption(string inputFile, string algorithmName, string outputFile)
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
                    throw new Exception(MainClass.Unknown);
            }
            algorithm.GenerateIV();
            algorithm.GenerateKey();

            var keyFile = string.Concat(inputFile.Contains('.') ? inputFile.Substring(0,
               inputFile.LastIndexOf('.')) : inputFile, FileEnd);
            var key = Convert.ToBase64String(algorithm.Key);
            var IV = Convert.ToBase64String(algorithm.IV);

            using (var outFile = new FileStream(keyFile, FileMode.Create))
            {
                using (var writer = new StreamWriter(outFile))
                {
                    writer.WriteLine(key);
                    writer.WriteLine(IV);
                }
            }
            using (FileStream inFile = new FileStream(inputFile, FileMode.Open),  outFile = new FileStream(outputFile, FileMode.Create))
            {
               using (var crypto = new CryptoStream(outFile, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
               {
                   inFile.CopyTo(crypto);
               }
            }
        }
    }
}

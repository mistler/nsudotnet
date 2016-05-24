using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.Enigma
{
    class Encryptor
    {
        private static Dictionary<string, Type> algorithmsDictionary = new Dictionary<string, Type>
        {
            {"aes", typeof(AesManaged)},
            {"dec", typeof(DESCryptoServiceProvider)},
            {"rc2", typeof(RC2CryptoServiceProvider)},
            {"rijndael", typeof(RijndaelManaged)}
        };

        private Type algorithmType;

        public Encryptor(string algorithm)
        {
            try
            {
                algorithmType = algorithmsDictionary[algorithm];
            }
            catch (KeyNotFoundException ex)
            {
                throw new ArgumentException("Not supported algorithm: " + algorithm);
            }

        }

        public void Encrypt(string inputFilename, string outputFilename)
        {
            using (FileStream inputStream = File.Open(inputFilename, FileMode.Open, FileAccess.Read),
                outputStream = File.Open(outputFilename, FileMode.Create, FileAccess.Write))
            {
                using (SymmetricAlgorithm algorithm = Activator.CreateInstance(algorithmType) as SymmetricAlgorithm)
                {
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                    // Create the streams used for encryption.
                    using (CryptoStream csEncrypt = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                    {
                        //Write all data to the stream.
                        inputStream.CopyTo(csEncrypt);
                    }

                    EncryptionKey key = new EncryptionKey(algorithm.Key, algorithm.IV);
                    using (FileStream keyStream = File.Open(inputFilename + ".key", FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter keyStreamWriter = new StreamWriter(keyStream))
                        {
                            keyStreamWriter.Write(key);
                        }
                    }
                }
            }
        }

        public void Decrypt(string inputFilename, string outputFilename, string keyFilename)
        {
            using (FileStream inputStream = File.Open(inputFilename, FileMode.Open, FileAccess.Read),
                outputStream = File.Open(outputFilename, FileMode.Create, FileAccess.Write),
                keyStream = File.Open(keyFilename, FileMode.Open, FileAccess.Read))
            {
                EncryptionKey key = null;
                using (StreamReader keyStreamReader = new StreamReader(keyStream))
                {
                    string IVString = keyStreamReader.ReadLine();
                    string KeyString = keyStreamReader.ReadLine();
                    key = new EncryptionKey(IVString, KeyString);
                }
                
                using (SymmetricAlgorithm algorithm = Activator.CreateInstance(algorithmType) as SymmetricAlgorithm)
                {
                    algorithm.Key = key.Key;
                    algorithm.IV = key.IV;
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                    // Create the streams used for decryption.
                    using (CryptoStream csDecrypt = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a outputStream.
                        csDecrypt.CopyTo(outputStream);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 4)   // Encryption.
            {
                string direction = args[0];
                string input = args[1];
                string algorithm = args[2];
                string output = args[3];

                if (direction == "encrypt")
                {
                    Archiver archiver = new Archiver(algorithm);
                    archiver.Encode(input, output);
                    return;
                }
            }
            else if (args.Length == 5)   // Decryption.
            {
                string direction = args[0];
                string input = args[1];
                string algorithm = args[2];
                string key = args[3];
                string output = args[4];

                if (direction == "decrypt")
                {
                    Archiver archiver = new Archiver(algorithm);
                    archiver.Decode(input, output, key);
                    return;
                }
            }

            System.Console.WriteLine("Incorrect argument count!");
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("\tEncryption: crypto.exe encrypt <input> <algorithm> <output>");
            System.Console.WriteLine("\tDecryption: crypto.exe decrypt <input> <algorithm> <key_file> <output>");
        }
    }
}

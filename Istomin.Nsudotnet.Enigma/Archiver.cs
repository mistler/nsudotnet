using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.Enigma
{
    internal enum Algorithm
    {
        AES,
        DEC,
        RC2,
        Rijndael
    }

    class Archiver
    {
        private static Dictionary<string, Algorithm> formatsDictionary = new Dictionary<string, Algorithm>
        {
            {"AES", Algorithm.AES},
            {"DEC", Algorithm.DEC},
            {"RC2", Algorithm.RC2},
            {"Rijndael", Algorithm.Rijndael}
        };

        private Algorithm algorithm;

        public Archiver(string algorithm)
        {
            try
            {
                this.algorithm = formatsDictionary[algorithm];
            }
            catch (KeyNotFoundException ex)
            {
                throw new ArgumentException("Not supported algorithm: " + algorithm);
            }

        }

        public void Encode(string input, string output)
        {
            using (FileStream inputStream = File.Open(input, FileMode.Open, FileAccess.Read),
                outputStream = File.Open(output, FileMode.Open, FileAccess.Write))
            {
                switch (algorithm)
                {
                    case Algorithm.AES:
                    case Algorithm.DEC:
                    case Algorithm.RC2:
                    case Algorithm.Rijndael:
                        throw new NotImplementedException("Sorry, but currently this algorithm is not implemented!");
                }
            }
        }

        public void Decode(string input, string output, string key)
        {
            using (FileStream inputStream = File.Open(input, FileMode.Open, FileAccess.Read),
                outputStream = File.Open(output, FileMode.Open, FileAccess.Write),
                keyStream = File.Open(key, FileMode.Open, FileAccess.Read))
            {
                switch (algorithm)
                {
                    case Algorithm.AES:
                    case Algorithm.DEC:
                    case Algorithm.RC2:
                    case Algorithm.Rijndael:
                        throw new NotImplementedException("Sorry, but currently this algorithm is not implemented!");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.Enigma
{
    class EncryptionKey
    {
        public EncryptionKey(string IV, string key)
        {
            this.IV = Convert.FromBase64String(IV);
            this.Key = Convert.FromBase64String(key);
        }

        public EncryptionKey(byte[] key, byte[] IV)
        {
            this.Key = key;
            this.IV = IV;
        }

        public override string ToString()
        {
            string IVString = Convert.ToBase64String(IV);
            string KeyString = Convert.ToBase64String(Key);
            string outputString = String.Format("{0}{1}{2}", IVString, Environment.NewLine, KeyString);
            return outputString;
        }

        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}

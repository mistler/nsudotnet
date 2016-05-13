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
        public EncryptionKey(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            int IVLength = BitConverter.ToInt32(bytes, 0);
            int keyLength = bytes.Length - sizeof(int) - IVLength;
            this.IV = new byte[IVLength];
            this.Key = new byte[keyLength];
            Array.Copy(bytes, sizeof(int), this.IV, 0, IVLength);
            Array.Copy(bytes, sizeof(int) + IVLength, this.Key, 0, keyLength);
        }

        public EncryptionKey(byte[] key, byte[] IV)
        {
            this.Key = key;
            this.IV = IV;
        }

        public override string ToString()
        {
            int IVLength = IV.Length;
            byte[] bytesIVLength = BitConverter.GetBytes(IVLength);
            IEnumerable<byte> rv = bytesIVLength.Concat(IV).Concat(Key);
            return Convert.ToBase64String(rv.ToArray());
        }

        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}

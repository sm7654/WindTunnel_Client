using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindTunnel_Client
{
    static class Encryption
    {
        private static Aes aesAlg;
        private static byte[] AESkey;
        private static byte[] AESiv;
        public static void GenerateKeys()
        {
            aesAlg = Aes.Create();
            aesAlg.KeySize = 256;
            aesAlg.GenerateKey();
            aesAlg.GenerateIV();
            AESkey = aesAlg.Key;
            AESiv = aesAlg.IV;

        }


    }
}

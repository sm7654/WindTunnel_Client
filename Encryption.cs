using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindTunnel_Client
{
    static class Encryption
    {
        private static byte[] AESkey;
        private static byte[] AESiv;
        public static (byte[], byte[]) GenerateKeys()
        {
            try
            {
                using (Aes aesServise = Aes.Create())
                {
                    aesServise.KeySize = 192;
                    AESkey = aesServise.Key;
                    AESiv = aesServise.IV;

                }
                byte[] aesKeyBytes = RsaEncryption.EncryptToMicro(AESkey.ToArray());
                byte[] aesIvBytes = RsaEncryption.EncryptToMicro(AESiv.ToArray());
                
                return (aesKeyBytes, aesIvBytes);
            }

            catch (Exception e)
            {
                return (null,null);
            }
         }

    }
}

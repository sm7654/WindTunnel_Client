using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindTunnel_Client
{
    static class RsaEncryption
    {
        private static string publicKey;
        private static string privateKey;
        private static string ServerPublickey;
        private static RSA Service;

        public static byte[] GenerateKeys()
        {
            Service = RSA.Create();
            publicKey = Service.ToXmlString(false);
            privateKey = Service.ToXmlString(true);
            return Encoding.UTF8.GetBytes(publicKey);
        }

        public static string Decrypt(byte[] data)
        {
            Service = RSA.Create();
            Service.FromXmlString(privateKey);

            return Encoding.UTF8.GetString(Service.Decrypt(data, RSAEncryptionPadding.Pkcs1));
        }

        public static byte[] EncryptToServer(byte[] data)
        {
            Service = RSA.Create();
            Service.FromXmlString(ServerPublickey);

            return Service.Encrypt(data, RSAEncryptionPadding.Pkcs1);
        }

        public static void SetServerPublicKey(string Temppublickey)
        {
            ServerPublickey = Temppublickey;
        }
    }
}

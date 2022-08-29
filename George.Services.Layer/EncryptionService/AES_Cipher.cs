#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
namespace George.Services.Layer.EncryptionService
{
    using System;
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;

    public static class AES_Cipher
    {
        private static readonly string DefualtKey = "Sonto_Aluswe";

        public static string EncryptText(string textToEncrypt)
        {
            try
            {
                string encytedText = "";
                string publickey = "12345678";

                var inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
                var secretkeyByte = Encoding.UTF8.GetBytes(DefualtKey);
                var publickeybyte = Encoding.UTF8.GetBytes(publickey);

                #pragma warning disable SYSLIB0021 // Type or member is obsolete
                using (var des = new DESCryptoServiceProvider())
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write))
                    {
                        cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                        cs.FlushFinalBlock();
                        encytedText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                #pragma warning restore SYSLIB0021 // Type or member is obsolete
                return encytedText;
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string DecryptText(string textToDecrypt)
        {
            try
            {
                string decryptedText = "";
                string publickey = "12345678";

                var privatekeyByte = Encoding.UTF8.GetBytes(DefualtKey);
                var publickeybyte = Encoding.UTF8.GetBytes(publickey);

                var inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));

                #pragma warning disable SYSLIB0021 // Type or member is obsolete
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write))
                    {
                        cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                        cs.FlushFinalBlock();
                        Encoding encoding = Encoding.UTF8;
                        decryptedText = encoding.GetString(ms.ToArray());
                    }
                }
                #pragma warning restore SYSLIB0021 // Type or member is obsolete
                return decryptedText;
            }
            catch (Exception ae)
            {
                throw new Exception(ae.Message, ae.InnerException);
            }
        }
    }
}

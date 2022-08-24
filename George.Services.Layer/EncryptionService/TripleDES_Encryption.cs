
namespace George.Services.Layer.EncryptionService
{
    using System.Text;
    using System.Security.Cryptography;

    public static class TripleDES_Encryption
    {
        private static readonly string DefualtKey = "Sonto_Aluswe";

        public static string Encrypt(string plainText, string encrptionKey)
        {
            using (var MyMD5CryptoService = new MD5CryptoServiceProvider())
            using (var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                MyTripleDESCryptoService.Key = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(encrptionKey));
                MyTripleDESCryptoService.Mode = CipherMode.ECB;
                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                //Encryption process
                using (var Encryptor = MyTripleDESCryptoService.CreateEncryptor())
                {
                    byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(plainText);
                    byte[] MyresultArray = Encryptor.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);

                    return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
                }
            }
        }

        public static string Decrypt(string cypherText, string decrptionKey)
        {
            using (var MyMD5CryptoService = new MD5CryptoServiceProvider())
            using (var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                MyTripleDESCryptoService.Key = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(decrptionKey));
                MyTripleDESCryptoService.Mode = CipherMode.ECB;
                MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                //Decryption process
                using (var Encryptor = MyTripleDESCryptoService.CreateDecryptor())
                {
                    byte[] MyDecryptArray = Convert.FromBase64String(cypherText);
                    byte[] MyresultArray = Encryptor.TransformFinalBlock(MyDecryptArray, 0, MyDecryptArray.Length);

                    MyTripleDESCryptoService.Clear();

                    return UTF8Encoding.UTF8.GetString(MyresultArray);
                }
            }
        }

        public static string GetEncryptionKey(string secretKey)
        {
            // MD5 is the hash algorithm expected by rave to generate encryption key
            var md5 = MD5.Create();

            // MD5 works with bytes so a conversion of plain secretKey to it bytes equivalent is required.
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            byte[] hashedSecret = md5.ComputeHash(secretKeyBytes, 0, secretKeyBytes.Length);
            byte[] hashedSecretLast12Bytes = new byte[12];

            Array.Copy(hashedSecret, hashedSecret.Length - 12, hashedSecretLast12Bytes, 0, 12);
            String hashedSecretLast12HexString = BitConverter.ToString(hashedSecretLast12Bytes);

            hashedSecretLast12HexString = hashedSecretLast12HexString.ToLower().Replace("-", "");

            String secretKeyFirst12 = secretKey.Replace("FLWSECK-", "").Substring(0, 12);

            byte[] hashedSecretLast12HexBytes = Encoding.UTF8.GetBytes(hashedSecretLast12HexString);
            byte[] secretFirst12Bytes = Encoding.UTF8.GetBytes(secretKeyFirst12);
            byte[] combineKey = new byte[24];

            Array.Copy(secretFirst12Bytes, 0, combineKey, 0, secretFirst12Bytes.Length);
            Array.Copy(hashedSecretLast12HexBytes, hashedSecretLast12HexBytes.Length - 12, combineKey, 12, 12);

            return Encoding.UTF8.GetString(combineKey);
        }

    }
}

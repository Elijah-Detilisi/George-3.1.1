

namespace George.Services.Layer.EncryptionService
{
    using System.Text;
    using System.Security.Cryptography;

    public static class PasswordHashing
    {
        public static string EncryptHashPassword(string password)
        {
            using (var sha_Algo = SHA256.Create())
            {
                var pwBytes = Encoding.Default.GetBytes(password); 
                var hashedPW = sha_Algo.ComputeHash(pwBytes);

                return Convert.ToBase64String(hashedPW);
            }
        }

    }
}

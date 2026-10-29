using System.Security.Cryptography; // SHA256 için
using System.Text;                  // encoding için yani bir stringi byte dizisine çevirmek için 

namespace StajYonetimSistemi.Services
{
    public class PasswordService
    {
        // düz şifreyi alır, SHA-256 ile hashlenmiş halini döndürür
        public string Hash(string password)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        // girilen şifreyi hashleyip veritabanındaki hash ile karşılaştırır
        public bool Verify(string hashedPassword, string enteredPassword)
        {
            return Hash(enteredPassword) == hashedPassword;
        }
    }
}
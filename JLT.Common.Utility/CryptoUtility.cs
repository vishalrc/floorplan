using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace JLT.Common.Utility
{
    public static class CryptoUtility
    {
        public static string Encrypt(string clearText, string encryptionKey, string salt)
        {            
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, saltBytes);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText, string encryptionKey, string salt)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, saltBytes);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string GenerateHash(string Text, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(Text);
            var base64 = Convert.ToBase64String(bytes);
            byte[] textBytes = Convert.FromBase64String(base64);
            byte[] saltBytes = Encoding.Unicode.GetBytes(salt);

            var hmacMD5 = new HMACMD5(saltBytes);
            var saltedHash = hmacMD5.ComputeHash(textBytes);

            return Encoding.Unicode.GetString(saltedHash.ToArray());
        }

        public static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "byte", Justification = "It really is a byte length")]
        public static string GenerateSalt(int SaltSize)
        {
            byte[] buf = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buf);
            }
            return Convert.ToBase64String(buf);
        }
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;


namespace SGProTexter
{
    internal class EncryptDecrypt
    {
#pragma warning disable CA1416 // Validate platform compatibility
        static public string userNameKey = WindowsIdentity.GetCurrent().Name;
#pragma warning restore CA1416 // Validate platform compatibility
        static private readonly string defaultKey = "b56dc5898b3e4122bbce2bb2515b1326";
        static private string passwordKey = "";

        private static string CreateKey()
        {
            int startOfStr;
            int endOfStr;

            if (userNameKey.Length < 1)
            {
                userNameKey = defaultKey;
            }
            string keyStr = userNameKey;
            if (keyStr.Contains("\\"))
            {
                startOfStr = keyStr.IndexOf("\\") + 1;
                if (startOfStr < keyStr.Length)
                {
                    endOfStr = (keyStr.Length - startOfStr);
                    keyStr = keyStr.Substring(startOfStr, endOfStr);
                }
            }
            // Convert string to HEX value
            byte[] bytes = Encoding.UTF8.GetBytes(keyStr);
            string hexString = Convert.ToHexString(bytes);
            // Combine the keyStr and defaultkey, then copy just the required length from the start of the combine to the need lenght only
            string ReturnString = hexString + defaultKey; // copy from the keyStr to orginal length only;
            ReturnString = ReturnString[..defaultKey.Length];
            return ReturnString;
        }

        public static string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            passwordKey = CreateKey();
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(passwordKey);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new((Stream)cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
                array = memoryStream.ToArray();
            }
            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            passwordKey = CreateKey();
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(passwordKey);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new((Stream)cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}


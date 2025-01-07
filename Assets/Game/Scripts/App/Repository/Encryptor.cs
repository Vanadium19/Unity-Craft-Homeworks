using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Game.Scripts.App.Repository
{
    public class Encryptor
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public Encryptor(string key, string iv)
        {
            _key = GetKey(key);
            _iv = GetIV(iv);
        }

        public string Encrypt(string text)
        {
            using Aes aes = Aes.Create();
            
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            
            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(text);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Decrypt(string text)
        {
            using Aes aes = Aes.Create();
            
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text));
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);

            return  streamReader.ReadToEnd();
        }

        private byte[] GetKey(string key)
        {
            using SHA256 sha256 = SHA256.Create();

            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }

        private byte[] GetIV(string iv)
        {
            using MD5 md5 = MD5.Create();

            return md5.ComputeHash(Encoding.UTF8.GetBytes(iv));
        }
    }
}
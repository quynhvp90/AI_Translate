using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GPT_Translate
{
    public static class Helper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012"); // 32 bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes
        public static List<SubtitleItem> ParseSubtitle(string input)
        {
            var result = new List<SubtitleItem>();

            var blocks = input.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var block in blocks)
            {
                var lines = block.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length >= 3)
                {
                    result.Add(new SubtitleItem
                    {
                        No = int.Parse(lines[0].Trim()),
                        Time = lines[1].Trim(),
                        Text = lines[2].Trim()
                    });
                }
            }

            return result;
        }
        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            var cipherBytes = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}

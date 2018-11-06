using System;
using System.Security.Cryptography;
using System.Text;

namespace Br.Utils
{
    public class Md5Utility
    {
        public static string Get(string text) {
            byte[] result = Encoding.Default.GetBytes(text);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
    }
}

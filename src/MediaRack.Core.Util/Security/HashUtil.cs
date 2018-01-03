using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MediaRack.Core.Util.Security
{
    public static class HashUtil
    {
        /// <summary>
        /// Compute hash for a given byte array
        /// </summary>
        /// <param name="data">Data to be hashed</param>
        /// <param name="algo">Algorithm to use E.g.: MD5, SHA, SHA256, SHA512</param>
        /// <returns></returns>
        public static string ComputeHash(byte[] data, string algo = "MD5")
        {
            algo = string.IsNullOrWhiteSpace(algo) ? "MD5" : algo;
            using (var md5 = HashAlgorithm.Create(algo))
            {
                byte[] inputBytes = data;
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        
        /// <summary>
        /// Compute hash for a given string, Provided string will be converted to a byte array using UTF8 encoding
        /// </summary>
        /// <param name="data">Data to be hashed</param>
        /// <param name="algo">Algorithm to use E.g.: MD5, SHA, SHA256, SHA512</param>
        /// <returns></returns>
        public static string ComputeHash(string data, string algo = "MD5")
        {
            algo = string.IsNullOrWhiteSpace(algo) ? "MD5" : algo;
            if (string.IsNullOrWhiteSpace(data)) return string.Empty;

            var utf8data = Encoding.UTF8.GetBytes(data);
            return ComputeHash(utf8data, algo);
        }
   
    
        /// <summary>
        /// Compute hash for a given byte array
        /// </summary>
        /// <param name="d">Data to be hashed</param>
        /// <param name="algo">Algorithm to use E.g.: MD5, SHA, SHA256, SHA512</param>
        /// <returns></returns>
        public static string Hash(this string d, string algo = "MD5")
        {
            return ComputeHash(d, algo);
        }

        /// <summary>
        /// Compute hash for a given string, Provided string will be converted to a byte array using UTF8 encoding
        /// </summary>
        /// <param name="d">Data to be hashed</param>
        /// <param name="algo">Algorithm to use E.g.: MD5, SHA, SHA256, SHA512</param>
        /// <returns></returns>
        public static string Hash(this byte[] d, string algo = "MD5")
        {
            return ComputeHash(d, algo);
        }

        public static string HashFile(string filePath, string algo = "MD5")
        {
            algo = string.IsNullOrWhiteSpace(algo) ? "MD5" : algo;
            using (var md5 = HashAlgorithm.Create(algo))
            {
                using (var file = System.IO.File.OpenRead(filePath))
                {
                    var hash = md5.ComputeHash(file);
                    var str = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    return str;
                }
            }
        }
    }
}

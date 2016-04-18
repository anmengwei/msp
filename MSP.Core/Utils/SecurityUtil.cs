using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Core.Utils
{
    public static class SecurityUtil
    {
        private readonly static byte[] DES_btKey = Encoding.UTF8.GetBytes("!@#$%^&*");
        private readonly static byte[] DES_btIV = Encoding.UTF8.GetBytes("20100808");
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DESEncrypt(string sourceString)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                using (var ms = new MemoryStream())
                {
                    var inData = Encoding.UTF8.GetBytes(sourceString);
                    try
                    {
                        using (var cs = new CryptoStream(ms, des.CreateEncryptor(DES_btKey, DES_btIV), CryptoStreamMode.Write))
                        {
                            cs.Write(inData, 0, inData.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                    catch
                    {
                        return sourceString;
                    }
                }
            }
            catch { }

            return "";

        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string DESDecrypt(string encryptedString)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                using (var ms = new MemoryStream())
                {
                    var inData = Convert.FromBase64String(encryptedString);
                    try
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(DES_btKey, DES_btIV), CryptoStreamMode.Write))
                        {
                            cs.Write(inData, 0, inData.Length);
                            cs.FlushFinalBlock();
                        }

                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                    catch
                    {
                        return encryptedString;
                    }
                }
            }
        }


        /// <summary>
        /// 转义Des解密后的字符
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string EscapeDESDecrypt(string encryptedString)
        {
            return Uri.EscapeDataString(DESDecrypt(encryptedString));
        }


        /// <summary>
        /// 转义Des加密后的字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string EscapeDESEncrypt(string sourceString)
        {
            return Uri.EscapeDataString(DESEncrypt(sourceString));
        }
        /// <summary>
        /// sha1算法
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string SHA1(string pass)
        {
            SHA1 Sha1 = System.Security.Cryptography.SHA1.Create();
            var data = Sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string MD5(string pass)
        {
            MD5 algorithm = System.Security.Cryptography.MD5.Create();
            var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass));
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));

            return sb.ToString();
        }
    }
}

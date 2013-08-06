using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Security.Cryptography;
namespace Js.Com.DEncrypt
{
    public class DEncrypt
    {
        // Methods
        public DEncrypt()
        {
        }
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] buffer1 = Encoding.Default.GetBytes("Js");
            return Js.Com.DEncrypt.DEncrypt.Decrypt(encrypted, buffer1);
        }

        public static string Decrypt(string original)
        {
            return Js.Com.DEncrypt.DEncrypt.Decrypt(original, "Js", Encoding.Default);
        }

        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider provider1 = new TripleDESCryptoServiceProvider();
            provider1.Key = Js.Com.DEncrypt.DEncrypt.MakeMD5(key);
            provider1.Mode = CipherMode.ECB;
            return provider1.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        public static string Decrypt(string original, string key)
        {
            return Js.Com.DEncrypt.DEncrypt.Decrypt(original, key, Encoding.Default);
        }


        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buffer1 = Convert.FromBase64String(encrypted);
            byte[] buffer2 = Encoding.Default.GetBytes(key);
            return encoding.GetString(Js.Com.DEncrypt.DEncrypt.Decrypt(buffer1, buffer2));
        }



        public static string Encrypt(string original)
        {
            return Js.Com.DEncrypt.DEncrypt.Encrypt(original, "Js");
        }



        public static byte[] Encrypt(byte[] original)
        {
            byte[] buffer1 = Encoding.Default.GetBytes("Js");
            return Js.Com.DEncrypt.DEncrypt.Encrypt(original, buffer1);
        }



        public static string Encrypt(string original, string key)
        {
            byte[] buffer1 = Encoding.Default.GetBytes(original);
            byte[] buffer2 = Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Js.Com.DEncrypt.DEncrypt.Encrypt(buffer1, buffer2));
        }



        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider provider1 = new TripleDESCryptoServiceProvider();
            provider1.Key = Js.Com.DEncrypt.DEncrypt.MakeMD5(key);
            provider1.Mode = CipherMode.ECB;
            return provider1.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        public static byte[] MakeMD5(byte[] original)
        {
            byte[] buffer1 = new MD5CryptoServiceProvider().ComputeHash(original);
            return buffer1;
        }
    }
}

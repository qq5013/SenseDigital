using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Security.Cryptography;
namespace Js.Com.DEncrypt
{
    public class RSACryption
    {
        // Methods
        public RSACryption()
        {

        }

        public bool GetHash(FileStream objFile, ref byte[] HashData)
        {
            HashData = HashAlgorithm.Create("MD5").ComputeHash(objFile);
            objFile.Close();
            return true;
        }

        public bool GetHash(FileStream objFile, ref string strHashData)
        {
            byte[] buffer1 = HashAlgorithm.Create("MD5").ComputeHash(objFile);
            objFile.Close();
            strHashData = Convert.ToBase64String(buffer1);
            return true;
        }

        public bool GetHash(string m_strSource, ref string strHashData)
        {
            HashAlgorithm algorithm1 = HashAlgorithm.Create("MD5");
            byte[] buffer1 = Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            byte[] buffer2 = algorithm1.ComputeHash(buffer1);
            strHashData = Convert.ToBase64String(buffer2);
            return true;
        }

        public bool GetHash(string m_strSource, ref byte[] HashData)
        {
            HashAlgorithm algorithm1 = HashAlgorithm.Create("MD5");
            byte[] buffer1 = Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = algorithm1.ComputeHash(buffer1);
            return true;
        }

        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(xmlPrivateKey);
            byte[] buffer1 = Convert.FromBase64String(m_strDecryptString);
            byte[] buffer2 = provider1.Decrypt(buffer1, false);
            return new UnicodeEncoding().GetString(buffer2);
        }

        public string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(xmlPrivateKey);
            byte[] buffer1 = provider1.Decrypt(DecryptString, false);
            return new UnicodeEncoding().GetString(buffer1);
        }

        public string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(xmlPublicKey);
            byte[] buffer1 = provider1.Encrypt(EncryptString, false);
            return Convert.ToBase64String(buffer1);
        }

        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(xmlPublicKey);
            byte[] buffer1 = new UnicodeEncoding().GetBytes(m_strEncryptString);
            byte[] buffer2 = provider1.Encrypt(buffer1, false);
            return Convert.ToBase64String(buffer2);
        }

        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            xmlKeys = provider1.ToXmlString(true);
            xmlPublicKey = provider1.ToXmlString(false);
        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {
            byte[] buffer2 = Convert.FromBase64String(p_strHashbyteDeformatter);
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter deformatter1 = new RSAPKCS1SignatureDeformatter(provider1);
            deformatter1.SetHashAlgorithm("MD5");
            byte[] buffer1 = Convert.FromBase64String(p_strDeformatterData);
            if (deformatter1.VerifySignature(buffer2, buffer1))
            {
                return true;
            }
            return false;
        }

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, string p_strDeformatterData)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter deformatter1 = new RSAPKCS1SignatureDeformatter(provider1);
            deformatter1.SetHashAlgorithm("MD5");
            byte[] buffer1 = Convert.FromBase64String(p_strDeformatterData);
            if (deformatter1.VerifySignature(HashbyteDeformatter, buffer1))
            {
                return true;
            }
            return false;
        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData)
        {
            byte[] buffer1 = Convert.FromBase64String(p_strHashbyteDeformatter);
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter deformatter1 = new RSAPKCS1SignatureDeformatter(provider1);
            deformatter1.SetHashAlgorithm("MD5");
            if (deformatter1.VerifySignature(buffer1, DeformatterData))
            {
                return true;
            }
            return false;
        }

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter deformatter1 = new RSAPKCS1SignatureDeformatter(provider1);
            deformatter1.SetHashAlgorithm("MD5");
            if (deformatter1.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            return false;
        }

        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            byte[] buffer1 = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter1 = new RSAPKCS1SignatureFormatter(provider1);
            formatter1.SetHashAlgorithm("MD5");
            byte[] buffer2 = formatter1.CreateSignature(buffer1);
            m_strEncryptedSignatureData = Convert.ToBase64String(buffer2);
            return true;
        }

        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            byte[] buffer1 = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter1 = new RSAPKCS1SignatureFormatter(provider1);
            formatter1.SetHashAlgorithm("MD5");
            EncryptedSignatureData = formatter1.CreateSignature(buffer1);
            return true;
        }

        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter1 = new RSAPKCS1SignatureFormatter(provider1);
            formatter1.SetHashAlgorithm("MD5");
            byte[] buffer1 = formatter1.CreateSignature(HashbyteSignature);
            m_strEncryptedSignatureData = Convert.ToBase64String(buffer1);
            return true;
        }

        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            RSACryptoServiceProvider provider1 = new RSACryptoServiceProvider();
            provider1.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter formatter1 = new RSAPKCS1SignatureFormatter(provider1);
            formatter1.SetHashAlgorithm("MD5");
            EncryptedSignatureData = formatter1.CreateSignature(HashbyteSignature);
            return true;
        }
    }
}

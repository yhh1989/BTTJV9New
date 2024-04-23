
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;

using System.Security.Cryptography; 

using System.Text;


namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// RSA 加密帮助器
    /// </summary>
    public class RsaHelper
    {

        /// <summary>
        /// 试用版解密数据
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static DateTime Decrypt(string cipherText)
        {
            var param = new CspParameters();
            param.KeyContainerName = "试用版";
        
            using (var rsa = new RSACryptoServiceProvider(param))
            {
                var encryptData = Convert.FromBase64String(cipherText);
                var decryptData = rsa.Decrypt(encryptData, true);
                var date = Encoding.UTF8.GetString(decryptData);
                if (DateTime.TryParse(date, out var time))
                {
                    return time;
                }

                return DateTime.Now.AddDays(-1);
            }
        }
        /// <summary>
        /// 试用版加密数据
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string Encryption(DateTime date)
        {
            var param = new CspParameters();
            param.KeyContainerName = "试用版";
            var express = date.ToString("d");
            using (var rsa = new RSACryptoServiceProvider(param))
            {
                var plainData = Encoding.UTF8.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptData = rsa.Encrypt(plainData, true);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptData);//将加密后的字节数组转换为字符串
            }
        }
       
    }
    /// <summary>
    /// RSA 加密
    /// </summary>
    public class RsaCrypt
    {
        /// <summary>
        /// 编码
        /// </summary>
        public static Encoding Encoding = new UTF8Encoding(false);

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPublicKeyEncrypt(string key, string content)
        {
            var data = Encoding.GetBytes(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var param = (AsymmetricKeyParameter)new PemReader(tr).ReadObject();
                engine.Init(true, param);
            }
            return Convert.ToBase64String(engine.ProcessBlock(data, 0, data.Length));
        }

        /// <summary>
        /// 公钥加密（无长度限制）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPublicKeyEncryptUnrestrictedLength(string key, string content)
        {
            var data = Encoding.GetBytes(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var param = (AsymmetricKeyParameter)new PemReader(tr).ReadObject();
                engine.Init(true, param);
            }
            var maxBlockSize = engine.GetInputBlockSize();
            if (data.Length <= maxBlockSize)
            {
                return Convert.ToBase64String(engine.ProcessBlock(data, 0, data.Length));
            }
            else
            {
                using (var dataStream = new MemoryStream(data))
                {
                    using (var encryptStream = new MemoryStream())
                    {
                        var buffer = new byte[maxBlockSize];
                        var blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        while (blockSize > 0)
                        {
                            var tempEncrypt = new byte[blockSize];
                            Array.Copy(buffer, 0, tempEncrypt, 0, blockSize);
                            var dataEncrypt = engine.ProcessBlock(tempEncrypt, 0, blockSize);
                            encryptStream.Write(dataEncrypt, 0, dataEncrypt.Length);
                            blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        }
                        return Convert.ToBase64String(encryptStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPrivateKeyEncrypt(string key, string content)
        {
            var data = Encoding.GetBytes(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var keyPair = (AsymmetricCipherKeyPair)new PemReader(tr).ReadObject();
                engine.Init(true, keyPair.Private);
            }
            return Convert.ToBase64String(engine.ProcessBlock(data, 0, data.Length));
        }

        /// <summary>
        /// 私钥加密（无长度限制）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPrivateKeyEncryptUnrestrictedLength(string key, string content)
        {
            var data = Encoding.GetBytes(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var keyPair = (AsymmetricCipherKeyPair)new PemReader(tr).ReadObject();
                engine.Init(true, keyPair.Private);
            }

            var maxBlockSize = engine.GetInputBlockSize();
            if (data.Length <= maxBlockSize)
            {
                return Convert.ToBase64String(engine.ProcessBlock(data, 0, data.Length));
            }
            else
            {
                using (var dataStream = new MemoryStream(data))
                {
                    using (var encryptStream = new MemoryStream())
                    {
                        var buffer = new byte[maxBlockSize];
                        var blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        while (blockSize > 0)
                        {
                            var tempEncrypt = new byte[blockSize];
                            Array.Copy(buffer, 0, tempEncrypt, 0, blockSize);
                            var dataEncrypt = engine.ProcessBlock(tempEncrypt, 0, blockSize);
                            encryptStream.Write(dataEncrypt, 0, dataEncrypt.Length);
                            blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        }
                        return Convert.ToBase64String(encryptStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPublicKeyDecrypt(string key, string content)
        {
            var data = Convert.FromBase64String(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var param = (AsymmetricKeyParameter)new PemReader(tr).ReadObject();
                engine.Init(false, param);
            }
            return Encoding.GetString(engine.ProcessBlock(data, 0, data.Length));
        }

        /// <summary>
        /// 公钥解密（无长度限制）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPublicKeyDecryptUnrestrictedLength(string key, string content)
        {
            var data = Convert.FromBase64String(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var param = (AsymmetricKeyParameter)new PemReader(tr).ReadObject();
                engine.Init(false, param);
            }

            var maxBlockSize = engine.GetInputBlockSize();
            if (data.Length <= maxBlockSize)
            {
                return Encoding.GetString(engine.ProcessBlock(data, 0, data.Length));
            }
            else
            {
                using (var dataStream = new MemoryStream(data))
                {
                    using (var decryptStream = new MemoryStream())
                    {
                        var buffer = new byte[maxBlockSize];
                        var blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        while (blockSize > 0)
                        {
                            var tempDecrypt = new byte[blockSize];
                            Array.Copy(buffer, 0, tempDecrypt, 0, blockSize);
                            var dataDecrypt = engine.ProcessBlock(tempDecrypt, 0, blockSize);
                            decryptStream.Write(dataDecrypt, 0, dataDecrypt.Length);
                            blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        }
                        return Encoding.GetString(decryptStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPrivateKeyDecrypt(string key, string content)
        {
            var data = Convert.FromBase64String(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var keyPair = (AsymmetricCipherKeyPair)new PemReader(tr).ReadObject();
                engine.Init(false, keyPair.Private);
            }
            return Encoding.GetString(engine.ProcessBlock(data, 0, data.Length));
        }

        /// <summary>
        /// 私钥解密（无长度限制）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RsaPrivateKeyDecryptUnrestrictedLength(string key, string content)
        {
            var data = Convert.FromBase64String(content);
            var engine = new Pkcs1Encoding(new RsaEngine());
            using (var tr = new StringReader(key))
            {
                var keyPair = (AsymmetricCipherKeyPair)new PemReader(tr).ReadObject();
                engine.Init(false, keyPair.Private);
            }

            var maxBlockSize = engine.GetInputBlockSize();
            if (data.Length <= maxBlockSize)
            {
                return Encoding.GetString(engine.ProcessBlock(data, 0, data.Length));
            }
            else
            {
                using (var dataStream = new MemoryStream(data))
                {
                    using (var decryptStream = new MemoryStream())
                    {
                        var buffer = new byte[maxBlockSize];
                        var blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        while (blockSize > 0)
                        {
                            var tempDecrypt = new byte[blockSize];
                            Array.Copy(buffer, 0, tempDecrypt, 0, blockSize);
                            var dataDecrypt = engine.ProcessBlock(tempDecrypt, 0, blockSize);
                            decryptStream.Write(dataDecrypt, 0, dataDecrypt.Length);
                            blockSize = dataStream.Read(buffer, 0, maxBlockSize);
                        }
                        return Encoding.GetString(decryptStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// 普通加密
        /// </summary>
        /// <returns></returns>
        [Obsolete("方法已经过时！", true)]
        public static string RsaEncrypt(string key, string content)
        {
            var param = new CspParameters();
            param.KeyContainerName = key;
            using (var rsa = new RSACryptoServiceProvider(2048, param))
            {
                var data = Encoding.GetBytes(content);
                var encryptData = rsa.Encrypt(data, true);
                return Convert.ToBase64String(encryptData);
            }
        }

        /// <summary>
        /// 普通解密
        /// </summary>
        /// <returns></returns>
        [Obsolete("方法已经过时！", true)]
        public static string RsaDecrypt(string key, string content)
        {
            var param = new CspParameters();
            param.KeyContainerName = key;
            using (var rsa = new RSACryptoServiceProvider(2048, param))
            {
                var encryptData = Convert.FromBase64String(content);
                var decryptData = rsa.Decrypt(encryptData, true);
                return Encoding.GetString(decryptData);
            }
        }
    }
}
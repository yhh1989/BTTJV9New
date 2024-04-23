using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
public class RSAUtil
{
    
    static log4net.ILog logger = log4net.LogManager.GetLogger("RSAUtil");
    public static String SignDataByPrivateKey(String inputString, string privateKey)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(inputString); var sha256 = new SHA256CryptoServiceProvider();
        byte[] rgbHash = sha256.ComputeHash(bytes);
        RSACryptoServiceProvider key = new RSACryptoServiceProvider();
        key.FromXmlString(RSAPrivateKeyJavaConvertToCsharp(privateKey));
        RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
        formatter.SetHashAlgorithm("SHA256");
        byte[] inArray = formatter.CreateSignature(rgbHash);
        return Convert.ToBase64String(inArray);
    }
    private static string RSAPrivateKeyJavaConvertToCsharp(string privateKey)
    {
        RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
        return string.Format("{0}{1} {2} {3}{4}{5}{6}{7}",
            Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
            Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Utils
{
    public static class Encrypter
    {
        private const string Hash = "Kimberly&EliasCompadresDeProyecto";


        public static byte[] Encrypt (string value)
        {
            try
            {
                byte[] valueToBytes = UTF8Encoding.UTF8.GetBytes(value);
                MD5 md5 = MD5.Create();
                TripleDES tripleDES = TripleDES.Create();
                tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                tripleDES.Mode = CipherMode.ECB;
                ICryptoTransform transform = tripleDES.CreateEncryptor();
                byte[] encryptedValue = transform.TransformFinalBlock(valueToBytes, 0, valueToBytes.Length);
                return encryptedValue;
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public static string Desencrypt(byte[] encryptedValue)
        {
            try
            {
                MD5 md5 = MD5.Create();
                TripleDES tripleDES = TripleDES.Create();
                tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                tripleDES.Mode = CipherMode.ECB;
                var transform = tripleDES.CreateDecryptor();
                byte[] encryptedToDesencrypted = transform.TransformFinalBlock(encryptedValue, 0, encryptedValue.Length);
                return UTF8Encoding.UTF8.GetString(encryptedToDesencrypted);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}

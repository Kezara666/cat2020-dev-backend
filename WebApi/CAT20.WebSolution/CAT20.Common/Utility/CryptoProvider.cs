using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CAT20.Common.Utility
{
    public class CryptoProvider
    {
        /// <summary>
        /// Provide Hash for the given text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int SALTLENGTHLIMIT = 32;
        public string GetHash(string text)
        {
            return this.ComputeHash(text);
        }

        /// <summary>
        /// Validate Hash
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool IsValidHash(string text)
        {
            string hash = this.ComputeHash(text);
            return hash == text ? true : false;
        }

        /// <summary>
        /// Compute Hash
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ComputeHash(string value)
        {
            string hashKey = string.Empty;
            MD5 md5hash = MD5.Create();
            //Get the Hash data for the password
            byte[] hashdata = md5hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            //Store Byte in string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hashdata)
            {
                stringBuilder.Append(b);
            }

            hashKey = stringBuilder.ToString();
            return hashKey;
        }

        /// <summary>
        /// Generate Random Salt Value For Password
        /// </summary>
        /// <returns></returns>
        public static byte[] GetSalt()
        {
            return GetSalt(SALTLENGTHLIMIT);
        }
        /// <summary>
        /// Geneate Random Salt Based on Salt Length
        /// </summary>
        /// <param name="maximumSaltLength"></param>
        /// <returns></returns>
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public byte[] GenerateSaltedHash(byte[] plainPassword, byte[] salt)
        {
            return GenerateSaltedHash(plainPassword, salt, 0);
        }

        public byte[] GenerateSaltedHash(byte[] plainPassword, byte[] salt, int start)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainPassword.Length + (salt.Length - (start))];

            for (int i = 0; i < plainPassword.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainPassword[i];
            }
            int inc = 1;
            for (int i = start + 1; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainPassword.Length + inc] = salt[i];
                inc++;
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public string GenerateSaltedHashString(string plainPassword, string slat, int id)
        {
            try
            {
                return Convert.ToBase64String(
                                   new CryptoProvider().GenerateSaltedHash(
                                       Encoding.UTF8.GetBytes(plainPassword), Convert.FromBase64String(slat), id % 3));
            }
            catch (Exception)
            {

                return "";
            }

        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);


            string key = "CAT20";
            // Get the key from config file

            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //Set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //Mode of operation. There are other 4 modes
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //Padding mode(If any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //Transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //Get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            string key = "CAT20";

            if (useHashing)
            {
                //If hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //If hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //Set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //Mode of operation. There are other 4 modes 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //Padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //Return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PieReports.Models;
using PTUtility.Classes;
using ViewModel.Login;

namespace PieReports.Encryption_Decryption
{
    public class EncryptionDecryption
    {
        public static bool AccessToken(psp_dsp_user_access_token pduat, string URL)
        {
            string serializeProfile = JsonConvert.SerializeObject(pduat);
            string data = EncryptionDecryption.Encrypt(serializeProfile);
            EncryptData Endata = new EncryptData();
            Endata.EncryptObject = data;
            string data1 = HttpCall.HttpPostMethod(URL, Endata);
            psp_dsp_user_access_token Dataobj = JsonConvert.DeserializeObject<psp_dsp_user_access_token>(data1);

            if (pduat != null)
            {

                if (Dataobj.web_session_id == null || pduat.web_session_id != Dataobj.web_session_id)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static string Encrypt(string data)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(data);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }
            return data;
        }

        public static string Decrypt(string data)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(data);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    data = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return data;
        }

        public static string EncryptMD5(string data)
        {
            PTCrypto md5 = new PTCrypto("PI", PTCrypto.CryptType.MD5);
            //   plain = "Kinjal@1107";
            string encrypted = md5.Encrypt(data); 
            return encrypted;
        }
        public static string DecryptMD5(string data)
        {
            string encrypted = "";


            PTCrypto md5 = new PTCrypto("PT@IT", PTCrypto.CryptType.MD5);
            //   plain = "Kinjal@1107";
            encrypted = md5.Decrypt(data);
            return encrypted;
        }

        public static int RandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        public static string RandomSpecialCharac(string[] symSet)
        {
            Random random = new Random();
            int start2 = random.Next(0, symSet.Length);

            return symSet[start2];
        }

        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var charac = (char)new Random().Next(offset, offset + lettersOffset);
                builder.Append(charac);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            //Special Characters Array
            string[] schars = { "!", "@", "#", "$", "%", "&", "*" }; 

            // 2-Letters lower case   
            passwordBuilder.Append(RandomString(2, true));

            // 1-Digit between 0 and 9  
            passwordBuilder.Append(RandomNumber(0, 9));

            //1 Special Charac
            
            passwordBuilder.Append(RandomSpecialCharac(schars));

            // 1-Digit between 0 and 9  
            passwordBuilder.Append(RandomNumber(0, 9));

            //1 Special Charac
            passwordBuilder.Append(RandomSpecialCharac(schars));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));

            return passwordBuilder.ToString();
        }
    }
}

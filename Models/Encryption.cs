using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings;

namespace RefactorThis.Models
{
    public class Encryption
    {
        public static string DecryptString(string stringToDecrypt)
        {
            string decrypted;
            try
            {
                byte[] b = Convert.FromBase64String(stringToDecrypt);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (Exception ex)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public static string EnryptString(string stringToEncrypt)
        {
            string encrypted;
            try 
            {
                byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
                encrypted = Convert.ToBase64String(b);   
            }
            catch(Exception ex)
            {
                encrypted = "";
            }

            return encrypted;
        }
    }
}

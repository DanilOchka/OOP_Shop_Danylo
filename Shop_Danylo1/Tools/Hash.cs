using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace Shop_Danylo
{
    //клас для хешування паролю
    public static class Hash
    {
        public static string HashPasswrd(string password)
        {
            byte[] tmpSource;
            byte[] tmpHash;

            //Create a byte array from source data
            tmpSource = ASCIIEncoding.ASCII.GetBytes(password);

            //Compute hash based on source data
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            int i;
            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

    }
}

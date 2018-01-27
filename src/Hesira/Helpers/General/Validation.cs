using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Hesira.Helpers.General
{
    public static class Validation
    {

        #region CNP

        public static bool CNPValidator(string cnp)
        {

            try
            {
                if (cnp.Length < 13) return false;

                int suma = Int32.Parse(cnp[0].ToString()) * 2 +
                    Int32.Parse(cnp[1].ToString()) * 7 +
                    Int32.Parse(cnp[2].ToString()) * 9 +
                    Int32.Parse(cnp[3].ToString()) * 1 +
                    Int32.Parse(cnp[4].ToString()) * 4 +
                    Int32.Parse(cnp[5].ToString()) * 6 +
                    Int32.Parse(cnp[6].ToString()) * 3 +
                    Int32.Parse(cnp[7].ToString()) * 5 +
                    Int32.Parse(cnp[8].ToString()) * 8 +
                    Int32.Parse(cnp[9].ToString()) * 2 +
                    Int32.Parse(cnp[10].ToString()) * 7 +
                    Int32.Parse(cnp[11].ToString()) * 9;
                int rest = suma % 11;

                bool valid = (rest < 10) && (rest.ToString() == cnp[12].ToString())
                             ||
                             (rest == 10) && (cnp[12] == '1');

                return valid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


        public static string Encrypt(this string password, string salt)
        {

            var byteSalt = salt != null ? Convert.FromBase64String(salt) : null;
            var randSalt = false;
            var randomSalt = new byte[8];

            if (salt == null)
            {
                using (var crng = new RNGCryptoServiceProvider())
                    crng.GetBytes(randomSalt);

                randSalt = true;
            }
            else if (salt.Length < 8)
            {
                throw new ArgumentException("Salt must be at least 8 bytes");
            }

            var hash = new SHA256Managed();

            byte[] bytePassword = new UTF8Encoding().GetBytes(password);

            if (randSalt)
            {
                var saltedPass = new byte[bytePassword.Length + 8];

                // Combine the salt and password
                Array.Copy(bytePassword, saltedPass, bytePassword.Length);
                Array.Copy(randomSalt, 0, saltedPass, bytePassword.Length, 8);

                for (int i = 0; i < 1000; i++)
                {
                    saltedPass = hash.ComputeHash(saltedPass);
                }

                var storedPass = new byte[saltedPass.Length + 8];

                // Prepend the unhashed salt to the hash for duplication purposes
                Array.Copy(randomSalt, storedPass, 8);
                Array.Copy(saltedPass, 0, storedPass, 8, saltedPass.Length);

                // Use a base 64 string for storage portability
                return Convert.ToBase64String(storedPass);

            }

            var saltedConfigPass = new byte[bytePassword.Length + byteSalt.Length];

            // Combine the salt and password
            Array.Copy(bytePassword, saltedConfigPass, bytePassword.Length);
            Array.Copy(byteSalt, 0, saltedConfigPass, bytePassword.Length, byteSalt.Length);

            for (int i = 0; i < 1000; i++)
            {
                saltedConfigPass = hash.ComputeHash(saltedConfigPass);
            }

            var storedPassConfigSalt = new byte[saltedConfigPass.Length + byteSalt.Length];

            // Prepend the unhashed salt to the hash for duplication purposes
            Array.Copy(byteSalt, storedPassConfigSalt, byteSalt.Length);
            Array.Copy(saltedConfigPass, 0, storedPassConfigSalt, byteSalt.Length, saltedConfigPass.Length);

            // Use a base 64 string for storage portability
            return Convert.ToBase64String(storedPassConfigSalt);
        }

    }


}
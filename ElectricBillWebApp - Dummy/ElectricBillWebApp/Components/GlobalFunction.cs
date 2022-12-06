using System.Security.Cryptography;
using System.Text;

namespace ElectricBillWebApp.Components
{
    public class GlobalFunction
    {
        public static string CreateMD5Hash(string input)
        {

            byte[] clearBytes;
            clearBytes = new UnicodeEncoding().GetBytes(input);
            byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            string hashedText = BitConverter.ToString(hashedBytes);
            return hashedText;

        }        
    }
}

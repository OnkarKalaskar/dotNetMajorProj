using System.Security.Cryptography;

namespace PopcornBackend.PasswordEncryption
{
    internal class ShaEncrypt
    {
        static SHA1 sha1 = new SHA1CryptoServiceProvider();

        public static Stream GenerateStreamFromString(string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            stream.Position = 0;
            return stream;

        }
        public static string EncryptString(string input)
        {
            Stream data = GenerateStreamFromString(input);
            Byte[] convertedData = sha1.ComputeHash(data);
            string convertedString = BitConverter.ToString(convertedData);
            return convertedString;
        }


    }
}

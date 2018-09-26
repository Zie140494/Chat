using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Vasya";
            Console.WriteLine(s);
            string hashS = MD5Hash(s);
            Console.WriteLine(hashS);
            Console.ReadLine();

            

        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}

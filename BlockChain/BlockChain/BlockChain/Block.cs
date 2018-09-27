using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain
{
    public class Block
    {
        private string previosHash { get; set; }
        private string  transaction { get; set; }
        private List<string> contens { get; set; }

        public Block(string previosHash, string transaction)
        {
            this.previosHash = previosHash;
            this.transaction = transaction;

            
            contens.Add(MD5Hash(transaction));
            contens.Add(previosHash);
        }
        private string MD5Hash(string input)
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

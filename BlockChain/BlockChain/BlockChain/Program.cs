using System;
using NBitcoin;

namespace BlockChain
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var key = new Key();
            string s = 
            Console.WriteLine(key.PubKey.GetAddress(Network.Main));
        }
    }
}

using System;
using System.IO;
using NBitcoin;
using Nethereum.Accounts;
using Nethereum.Hex.HexConvertors.Extensions;

namespace GenerateWallets
{
    class Program
    {
        static void Main(string[] args)
        {
            // Number of BTC and ETH keys to generate
            int n = 100;

            Console.WriteLine("");

            // Generate Bitcoin addresses and private keys
            Console.WriteLine("Generating " + n + " BTC addresses & private keys...");
            Console.WriteLine("");

            string BTCfileName = "C:\\GENKEYS\\BitcoinKeys.csv";
            if (!File.Exists(BTCfileName))
            {
                string header = "ID, BTC address, BTC privateKey, Assigned" + Environment.NewLine;
                File.WriteAllText(BTCfileName, header);
            }
            for (int i = 1; i <= n; i++)
            {
                try
                {
                    Guid guid = Guid.NewGuid();

                    Key privateKey = new Key(); // generate a random private key
                    PubKey publicKey = privateKey.PubKey;   // derive public key

                    var privateKeySecret = privateKey.GetBitcoinSecret(Network.Main);
                    var address = publicKey.Decompress().GetAddress(Network.Main);

                    string keys = guid + "," + address + "," + privateKeySecret + ", 0" + Environment.NewLine;

                    File.AppendAllText(BTCfileName, keys);

                    Console.WriteLine("BTC address: " + address);
                }
                catch (Exception x)
                {
                    Console.WriteLine("Error:" + x.Message);
                    Console.WriteLine("");
                }
            }

            // --------------------------------------------
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            // --------------------------------------------

            // Generate Ethereum addresses and private keys
            Console.WriteLine("Generating " + n + " ETH addresses & private keys...");
            Console.WriteLine("");

            string ETHfileName = "C:\\GENKEYS\\EthereumKeys.csv";
            if (!File.Exists(ETHfileName))
            {
                string header = "ID, ETH address, ETH privateKey, Assigned" + Environment.NewLine;
                File.WriteAllText(ETHfileName, header);
            }
            for (int i = 1; i <= n; i++)
            {
                try
                {
                    Guid guid = Guid.NewGuid();

                    var ecKey = EthECKey.GenerateKey(); // generate a random private key
                    var privateKeyAsBytes = ecKey.GetPrivateKeyAsBytes().ToHex();   // derive public key
                    var address = ecKey.GetPublicAddress();

                    string keys =  guid + "," + address + "," + privateKeyAsBytes + ", 0" + Environment.NewLine;

                    File.AppendAllText(ETHfileName, keys);

                    Console.WriteLine("ETH address: " + address);
                }
                catch (Exception x)
                {
                    Console.WriteLine("Error:" + x.Message);
                    Console.WriteLine("");
                }
            }
        }
    }
}

using System;
using System.IO;

 namespace NBitcoin.Accounts
{
    public class BTCaccountCreator
    {
        public string CreateAccount(string password, string path)
        {
            BitcoinPassphraseCode passphrase = new BitcoinPassphraseCode(password, Network.Main, null);
            var generate = passphrase.GenerateEncryptedSecret();
            var BTCprivateKey = generate.EncryptedKey;
            var BTCaddress = generate.GeneratedAddress;
            var fileName = BTCaddress + ".key";
            // Save to File
            using (var newfile = File.CreateText(Path.Combine(path, fileName)))
            {
                Guid guid = Guid.NewGuid();
                string json = "{" +
                    "\"crypto\":{" +
                        "\"cipher\":\"\"," +
                        "\"ciphertext\":\"" + passphrase + "\"," +
                        "\"cipherparams\":{" +
                            "\"iv\":\"" + password + "\"" +
                        "}," +
                        "\"kdf\":\"\"," +
                        "\"mac\":\"\"," +
                        "\"kdfparams\":{" +
                            "\"n\":\"\"," +
                            "\"r\":\"\"," +
                            "\"p\":\"\"," +
                            "\"dklen\":\"\"," +
                            "\"salt\":\"" + BTCprivateKey + "\"" +
                        "}" +
                    "}," +
                    "\"id\":\"" + guid + "\"," +
                    "\"address\":\"" + BTCaddress + "\"," +
                    "\"version\":\"" +
                "}";
                newfile.Write(json);
                newfile.Flush();
            }
            return fileName;
        }
    }
}
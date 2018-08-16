using Nethereum.KeyStore;
using System.IO;

 namespace Nethereum.Accounts
{
    public class ETHaccountCreator
    {
        //[Fact]
        public void ShouldCreateKeyPair()
        {
            var ecKey = EthECKey.GenerateKey();
            //Get the public address (derivied from the public key)
            var address = ecKey.GetPublicAddress();
            var privateKey = ecKey.GetPrivateKey();
        }

        public string CreateAccount(string password, string path)
        {
            //Generate a private key pair using SecureRandom
            var ecKey = EthECKey.GenerateKey();
            //Get the public address (derivied from the public key)
            var address = ecKey.GetPublicAddress();

            //Create a store service, to encrypt and save the file using the web3 standard
            var service = new KeyStoreService();
            var encryptedKey = service.EncryptAndGenerateDefaultKeyStoreAsJson(password, ecKey.GetPrivateKeyAsBytes(), address);
            //var fileName = service.GenerateUTCFileName(address) + ".key";
            var fileName = address + ".key";
            //save the File
            using (var newfile = File.CreateText(Path.Combine(path, fileName)))
            {
                newfile.Write(encryptedKey);
                newfile.Flush();
            }
            #region fileinfo
            /*
            {
                "crypto":{
                    "cipher":"aes-128-ctr",
                    "ciphertext":"e2333eb119176b6e99dff8c94707ea6024384d86d2170d93a4255eca0fd76d44",
                    "cipherparams":{
                        "iv":"02517e19957e32cb4fd8c61220f3d324"
                    },
                    "kdf":"scrypt",
                    "mac":"03d47048ac152955bba487819fa9bd6699532eaef9a3b92d72ef7e82731dbf45",
                    "kdfparams":{
                        "n":262144,
                        "r":1,
                        "p":8,
                        "dklen":32,
                        "salt":"a0d67c55d1523054c01d68a066b504686deec489a57be62f7b09b49b61fc676b"
                    }
                },
                "id":"8ce7c2d6-35da-47d5-977c-7f49996159c4",
                "address":"0x191F3b72b848BB987091562Ef23FFE8e4Ba8340f",
                "version":3
            }
            */
            #endregion
            return fileName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;
/// <summary>
/// Basic class setup comes from http://www.c-sharpcorner.com/uploadfile/4088a7/using-cryptography-in-portable-xamarin-formswindows-phone/
/// The class basically encrypts and decrypts data based on a password, a salt and on AESCBC PKCS7 algorithm
/// @created Marwin Philips
/// </summary>
namespace SmaNa.LocalDataAccess
{
    public class Encrypter
    {
        private string _password;
        private byte[] _salt;
        private byte[] _key;
        private ICryptographicKey _symetricKey;
        public Encrypter(string Password)
        {
            _password = Password;
            _salt = new byte[]{ 16,30,45,1,17,15,240, 193};
            _key = CreateDerivedKey();

            ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            _symetricKey = aes.CreateSymmetricKey(_key);
        }

        /// <summary>    
        /// Creates a derived key from a comnination     
        /// </summary>
        /// <param name="keyLengthInBytes"></param>    
        /// <param name="iterations"></param>    
        /// <returns></returns>    
        public byte[] CreateDerivedKey(int keyLengthInBytes = 32, int iterations = 1000)
        {
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(_password, _salt, iterations, keyLengthInBytes);
            return key;
        }

        /// <summary>    
        /// Encrypts given data using symmetric algorithm AES    
        /// </summary>    
        /// <param name="data">Data to encrypt</param>    
        /// <param name="password">Password</param>    
        /// <param name="salt">Salt</param>    
        /// <returns>Encrypted bytes</returns>    
        public string EncryptAes(string data)
        {
            var bytes = WinRTCrypto.CryptographicEngine.Encrypt(_symetricKey, Encoding.Unicode.GetBytes(data));
            return Convert.ToBase64String(bytes);
        }
        /// <summary>    
        /// Decrypts given bytes using symmetric alogrithm AES    
        /// </summary>    
        /// <param name="data">data to decrypt</param>    
        /// <returns></returns>    
        public string DecryptAes(string data)
        {
            var bytes = WinRTCrypto.CryptographicEngine.Decrypt(_symetricKey, Convert.FromBase64String(data));
            return Encoding.Unicode.GetString(bytes,0, bytes.Length);
        }
    }
}

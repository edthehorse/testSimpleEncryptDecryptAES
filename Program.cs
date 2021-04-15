using System;
using System.Text;
using System.Security.Cryptography;


    class Program
    {
    private static byte[] iv; // iVE MADE THIS A GLOBAL VARIABLE?

    static void Main(string[] args)
        {
    
        // Define the message to be encrypted (and decrypted) and print it for starters.
        string message = "Attack at dawn!"; Console.WriteLine("Original: {0}", message);

        // Make an arbitray key for encryption.
        byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // Must be 16 (or 32?) bytes long.
        
        // ENCRYPT:
        byte[] encodedBytes= testEncrypt(message,key); // The vector produced by the encryption must be returned for the DEcryption.
        
        // DECRYPT:
        byte[] decryptedBytes = testDecrypt(encodedBytes, key); // The vector produced by the encryption must be returned for the DEcryption.

       string EncDec = Encoding.UTF8.GetString(decryptedBytes);

        Console.WriteLine("tHE ENCRYPTED/dECRYPTED STRING IS: {0} " ,EncDec);
      }

    static byte[] testEncrypt(string mess, byte[] key)
    {
         //  We must ENCODE the bytes to UTF8 first.
         byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(mess); // needs System.Text for Encoding. 65 116 116 etc

        //Create a new instance of the default Aes implementation class  and configure encryption key.
         using Aes aes = Aes.Create();
        
        aes.Key = key;// Pass in the key to the aes object.
        //The aes also creates a vector (will also used for encrytion).(Retrieve it for the future DEcryption.)
       
        iv = aes.IV;

        var encryptor = aes.CreateEncryptor();

        //Now ENCRYPT:
        byte[] encryptedBytes= encryptor.TransformFinalBlock(bytesToEncrypt,0, bytesToEncrypt.Length);

        return encryptedBytes;
      }

    static byte[] testDecrypt(byte[] encryptedBytes, byte[] key)
    {
        using Aes aes = Aes.Create();

        aes.Key = key;

        aes.IV = iv;

        var decryptor = aes.CreateDecryptor();

        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return decryptedBytes;
    }
}











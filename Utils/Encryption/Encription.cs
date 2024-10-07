using System.Security.Cryptography;
using System.Text;

public static class Encription
{
    public static string DecryptConnectionString(string encryptedConnectionString, string encryptionKey)
    {
        // Método de desencriptación similar al que tienes en el controlador
        byte[] fullCipher = Convert.FromBase64String(encryptedConnectionString);

        byte[] iv = new byte[16];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        using (Aes aes = Aes.Create())
        {
            aes.Key = GenerateKey(encryptionKey); // Usar la clave proporcionada como parámetro
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(cipher))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd(); // Devolver cadena desencriptada
                    }
                }
            }
        }
    }

    // Método para generar una clave de 32 bytes usando SHA256
    private static byte[] GenerateKey(string key)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key)); // Generar clave de 32 bytes
        }
    }
}

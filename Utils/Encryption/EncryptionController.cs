using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoVentas_Back.DataAccess.Encryption
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        // La clave de encriptación, asegúrate de almacenarla en un lugar seguro
        private readonly string encryptionKey = "Desarrollo";

        // POST: api/encryption/encrypt
        [HttpPost]
        [Route("encrypt")]
        public IActionResult Encrypt([FromBody] EncryptionRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PlainText))
            {
                return BadRequest("No se proporcionó ningún texto para encriptar.");
            }

            // Encriptar el texto
            var encryptedText = EncryptString(request.PlainText);

            return Ok(new { EncryptedText = encryptedText });
        }

        // POST: api/encryption/decrypt
        [HttpPost]
        [Route("decrypt")]
        public IActionResult Decrypt([FromBody] EncryptionRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PlainText))
            {
                return BadRequest("No se proporcionó ningún texto para desencriptar.");
            }

            // Desencriptar el texto
            var decryptedText = DecryptString(request.PlainText);

            return Ok(new { DecryptedText = decryptedText });
        }

        // Método para encriptar una cadena de texto
        private string EncryptString(string plainText)
        {
            byte[] iv = new byte[16]; // IV de 128 bits
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = GenerateKey(encryptionKey); // Generar clave de 32 bytes usando SHA256
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(iv); // Generar IV aleatorio
                }
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            // Devuelve el IV junto con el texto cifrado
            byte[] result = new byte[iv.Length + array.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(array, 0, result, iv.Length, array.Length);

            return Convert.ToBase64String(result);
        }

        // Método para desencriptar una cadena de texto
        private string DecryptString(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            // Separar el IV del texto cifrado
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GenerateKey(encryptionKey); // Generar la misma clave
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(cipher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd(); // Leer el texto desencriptado
                        }
                    }
                }
            }
        }

        // Método para generar una clave de 32 bytes usando SHA256
        private byte[] GenerateKey(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key)); // Generar clave de 32 bytes
            }
        }
    }

    // Clase de solicitud para enviar el texto a encriptar/desencriptar
    public class EncryptionRequest
    {
        public string PlainText { get; set; }
    }
}

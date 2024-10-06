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
        // La clave de encriptación, deberías almacenarla de forma segura (e.g., variable de entorno)
        private readonly string encryptionKey = "clave_super_segura_y_muy_segura123";

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

        // Método para encriptar una cadena de texto
        private string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
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

            return Convert.ToBase64String(array);
        }
    }

    // Clase de solicitud para enviar el texto a encriptar
    public class EncryptionRequest
    {
        public string PlainText { get; set; }
    }
}

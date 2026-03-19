using System.Security.Cryptography;
using System.Text;
using System;

namespace Vivigest_backend.Application.Utilities
{
    public class PasswordManager
    {
        public static (string PasswordHash, string PasswordSalt) generatePassword(string password)
        {
            using var hmac = new HMACSHA512();

            var saltBytes = hmac.Key;
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
        }

        public static bool verifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            var hashBytes = Convert.FromBase64String(storedHash);
            var saltBytes = Convert.FromBase64String(storedSalt);

            // Inicializamos el algoritmo usando el Salt que trajimos de la BD
            using var hmac = new HMACSHA512(saltBytes);

            // Hasheamos la contraseña que el usuario escribió en el Login
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Comparamos byte por byte
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hashBytes[i]) return false;
            }

            return true;
        }
    }
}
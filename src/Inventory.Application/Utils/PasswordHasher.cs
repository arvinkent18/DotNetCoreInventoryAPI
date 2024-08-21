using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Inventory.Application.Utils
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int _saltSize = 16;
        private const int _hashSize = 32;
        private const int _iterations = 10000;

        public string HashPassword(string password)
        {
            byte[] salt = new byte[_saltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: _iterations,
                numBytesRequested: _hashSize
            ));
            return $"{Convert.ToBase64String(salt)}:{hashedPassword}";
        }

        bool IPasswordHasher.VerifyPassword(string hashedPassword, string password)
        {
            var parts = hashedPassword.Split(':');
            if (parts.Length != 2) return false;

            var storedSalt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var providedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: _iterations,
                numBytesRequested: _hashSize
            ));

            return storedHash == providedHash;
        }
    }
}

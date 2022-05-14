using System;
using System.Security.Cryptography;
using System.Text;

namespace Volunteer.Common.Crypto
{
    public class Sha256PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;

        string IPasswordHasher.Hash(string password)
        {
            var salt = CreateSalt();
            var saltedPassword = HashInternal($"{salt}{password}");
            return $"{salt}.{saltedPassword}";
        }

        bool IPasswordHasher.Verify(string password, string hash)
        {
            var parts = hash.Split('.');
            var salt = parts[0];
            var hashedPassword = parts[1];

            return hashedPassword == HashInternal($"{salt}{password}");
        }

        private static string HashInternal(string text)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(bytes);
        }

        private static string CreateSalt()
        {
            using var random = RandomNumberGenerator.Create();
            Span<byte> salt = stackalloc byte[SaltSize];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

    }
}

namespace Volunteer.Common.Crypto
{
    public class DumbPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return password;
        }

        public bool Verify(string password, string hash)
        {
            return password == hash;
        }
    }
}

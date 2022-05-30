using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Services.Auth.Token
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}

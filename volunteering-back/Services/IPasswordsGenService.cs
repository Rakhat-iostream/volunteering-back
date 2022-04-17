using Vol.Services;
using IdentityModel;
using System.Linq;
using System.Threading.Tasks;

namespace Vol.Services
{
    public interface IPasswordsGenService
    {
        Task<string> GetRandomAlphaNumeric();
    }
}

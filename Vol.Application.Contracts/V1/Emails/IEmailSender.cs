using System.Threading.Tasks;

namespace Vol.V1.Emails
{
    public interface IEmailSender
    {
        Task SendAsync(string address, string subject, string body);
    }
}
using System.Threading.Tasks;

namespace Vol.V1.Emails
{
    public interface ITemplateRenderer
    {
        Task<string> RenderAsync(string templateName, object model, string cultureName);
    }
}
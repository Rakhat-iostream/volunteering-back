using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Infrastructure
{
    public interface ILanguageSwitcherService
    {
        Task<string> SetLanguage(string language);
    }
}

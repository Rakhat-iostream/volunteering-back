using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Infrastructure
{
    public class LanguageSwitcherService : ILanguageSwitcherService
    {

        public async Task<string> SetLanguage(string language)
        {
            var input = new CultureSwitcherModel();
            string res = null;

            CultureInfo lang = new CultureInfo(language);

            if (input.SupportedLanguages.Contains(lang.TwoLetterISOLanguageName))
            {
                res = language;
            }
            else
            {
                language = input.DefaultLanguage;
                res = language;
            }
            return res;
        }
    }
}

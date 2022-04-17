using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Infrastructure
{
    public class CultureSwitcherModel
    {
        public string DefaultLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; }

        public CultureSwitcherModel()
        {
            DefaultLanguage = "ru";
            SupportedLanguages = new List<string>
            {
                "en",
                "ru",
                "kz"
            };
        }
    }
}

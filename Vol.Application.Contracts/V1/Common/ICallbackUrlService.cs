using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Common
{
    public interface ICallbackUrlService
    {
        string GetUrl(CallbackUrlModel model, object value);
    }
}

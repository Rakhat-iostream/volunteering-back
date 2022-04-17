using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Events
{
    public interface IEventEmitter
    {
        Task SendEvent<TEvent>(Guid userId, IEventModel<TEvent> @event);
    }
}

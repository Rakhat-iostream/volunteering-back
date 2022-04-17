using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Infrastructure
{
    public interface IAggregateRoot<T>
    {
        public T Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

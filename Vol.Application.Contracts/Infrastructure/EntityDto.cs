﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Infrastructure
{
    public class EntityDto<T>
    {
        public T Id { get; protected set; }

        public DateTime CreationDate { get; set; }

        protected EntityDto(T id)
        {
            Id = id;
        }

        protected EntityDto()
        {
        }
    }
}

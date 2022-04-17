using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Infrastructure
{
    public class ApplicationService : IApplicationService
    {
        protected readonly IMapper mapper;

        public ApplicationService(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}

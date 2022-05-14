using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models
{
    public class PageRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }

        public static PageRequest Default() => new PageRequest() { Skip = 0, Take = 10 };
    }
}

using System.Collections.Generic;

namespace Volunteer.Common.Models
{
    public class PageResponse<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Result { get; set; }
    }
}

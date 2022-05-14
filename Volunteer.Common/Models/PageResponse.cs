using System.Collections.Generic;

namespace Volunteer.Common.Models
{
    public class PageResponse<T>
    {
        public PageResponse(int total, List<T> result)
        {
            Total = total;
            Result = result;
        }

        public int Total { get; }
        public List<T> Result { get; }
    }
}

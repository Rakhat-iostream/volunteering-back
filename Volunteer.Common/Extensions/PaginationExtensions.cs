using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volunteer.Common.Models;

namespace Volunteer.Common.Extensions
{
    public static class PaginationExtensions
    {
       /* public static async Task<PageResponse<T>> ToPageAsync<T>(
            this IQueryable<T> query,
            PageRequest pageRequest,
            CancellationToken cancellationToken = default)
        {
            int total = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip(pageRequest.Skip)
                .Take(pageRequest.Take)
                .ToListAsync(cancellationToken);

            return new PageResponse<T>(total, items);
        }*/
    }
}

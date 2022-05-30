using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories
{
    public interface INewsRepository
    {
        public ICollection<News> GetAll(PageRequest request);
        public Task<News> GetAsync(int id);
        public Task<News> CreateAsync(News news);
        public Task<News> UpdateAsync(News news);
        public Task DeleteAsync(int id);
    }
}

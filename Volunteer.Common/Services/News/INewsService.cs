using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.News;

namespace Volunteer.Common.Services
{
    public interface INewsService
    {
        public Task<News> GetAsync(int id);
        public Task<PageResponse<News>> GetAll(PageRequest request);
        public Task<News> CreateAsync(NewsAddOrUpdateDto dto);
        public Task<News> UpdateAsync(NewsAddOrUpdateDto dto);
        public Task DeleteAsync(int id);
    }
}

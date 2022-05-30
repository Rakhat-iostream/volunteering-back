using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Repositories;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly VolContext _db;

        public NewsRepository(VolContext db)
        {
            _db = db;
        }

        public ICollection<News> GetAll(PageRequest request)
        {
            var entity = _db.News.ToList();
            return entity;
        }

        public async Task<News> GetAsync(int id)
        {
            var news = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            return news;
        }

        public async Task<News> CreateAsync(News news)
        {
            if (news != null)
            {
                await _db.News.AddAsync(news);
                await _db.SaveChangesAsync();
            }

            return news;
        }

        public async Task<News> UpdateAsync(News news)
        {
            var entity = await _db.News.FirstOrDefaultAsync(x => x.Id == news.Id);

            entity.Topic = news.Topic ?? entity.Topic;
            entity.Author = news.Author ?? entity.Author;
            entity.Image = news.Image ?? entity.Image;
            entity.Content = news.Content ?? entity.Content;

            var result = _db.News.Update(entity);
            await _db.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.News.FirstOrDefaultAsync(x => x.Id == id);
            _db.News.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}

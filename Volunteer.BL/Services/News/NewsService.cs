using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.News;
using Volunteer.Common.Repositories;
using Volunteer.Common.Services;

namespace Volunteer.BL.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;

        public NewsService(INewsRepository newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        public async Task<News> GetAsync(int id)
        {
            var events = await _newsRepository.GetAsync(id);
            return events;
        }

        public async Task<PageResponse<News>> GetAll(PageRequest request)
        {
            var news = _newsRepository.GetAll(request);

            var total = news.Count();

            var result = news.Skip(request.Skip).Take(request.Take);

            return new PageResponse<News>
            {
                Total = total,
                Result = result

            };
        }

        public async Task<News> CreateAsync(NewsAddOrUpdateDto dto)
        {
            News news = new News();
            news.Id = new int();
            news.Topic = dto.Topic;
            news.Author = dto.Author;
            news.Image = dto.Image;
            news.Content = dto.Content;

            await _newsRepository.CreateAsync(news);

            return news;

        }

        public async Task<News> UpdateAsync(NewsAddOrUpdateDto dto)
        {
            var news = _mapper.Map<NewsAddOrUpdateDto, News>(dto);

            await _newsRepository.UpdateAsync(news);

            return news;
        }

        public async Task DeleteAsync(int id)
        {
            await _newsRepository.DeleteAsync(id);
        }
    }
}

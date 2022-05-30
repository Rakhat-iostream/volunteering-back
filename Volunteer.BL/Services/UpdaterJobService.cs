using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Services;
using Volunteer.Dal.SqlContext;

namespace Volunteer.BL.Services
{
    public class UpdaterJobService : IScopedProcessingService
    {
        private readonly ILogger<UpdaterJobService> _logger;
        private VolContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public UpdaterJobService(ILogger<UpdaterJobService> logger, VolContext context, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<VolContext>();

            var entities = _context.Events
                .Where(item => item.IsFinished == false && DateTime.Now > item.EndDate).ToList();

            if (!entities.Any())
            {
                _logger.LogInformation("Просроченных событий нет.");
                return;
            }

            try
            {
                foreach (var entity in entities)
                {
                    entity.IsFinished = true;

                    _context.Update(entity);
                    _context.SaveChanges();

                    _logger.LogInformation($"Событие под этим номером просрочена: {entity.EventId}");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestCheckerService exception");
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}

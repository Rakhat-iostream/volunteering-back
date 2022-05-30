using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Services;
using Volunteer.Common.Services.Events;

namespace Volunteer.BL.Services
{
    public class GetExcelService : IGetExcelService
    {
        private readonly IEventService _eventService;
        private readonly IExcelGenerator _excelGenerator;

        public GetExcelService(IEventService eventService, 
            IExcelGenerator excelGenerator)
        {
            _eventService = eventService;
            _excelGenerator = excelGenerator;
        }

        public async Task<byte[]> GetVolunteerReport(EventClientRequest request)
        {
            request.Skip = 0;
            request.Take = 10000;
            var attendedVolunteers = await _eventService.GetEventAttenders(request);
            var excel = _excelGenerator.GenerateReport(attendedVolunteers);
            return excel;
        }
    }
}

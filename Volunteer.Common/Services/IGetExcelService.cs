using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.ClientRequests;

namespace Volunteer.Common.Services
{
    public interface IGetExcelService
    {
        public Task<byte[]> GetVolunteerReport(EventClientRequest request);
    }
}

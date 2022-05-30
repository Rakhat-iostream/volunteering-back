using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.DTOs.Volunteers;

namespace Volunteer.Common.Services
{
    public interface IExcelGenerator
    {
        public byte[] GenerateReport(PageResponse<VolunteerProfileDto> models);
    }
}

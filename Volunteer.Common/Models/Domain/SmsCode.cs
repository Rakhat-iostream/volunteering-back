using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.Domain
{
    public class SmsCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

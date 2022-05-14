using System;

namespace Volunteer.Common.Models.Domain
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

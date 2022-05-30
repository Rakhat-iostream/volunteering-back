using System;
using System.ComponentModel.DataAnnotations;

namespace Volunteer.Common.Models.Domain
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public string Author { get; set; }
    }
}

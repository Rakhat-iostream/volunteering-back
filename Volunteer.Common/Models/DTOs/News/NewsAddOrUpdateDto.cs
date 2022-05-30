using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.DTOs.News
{
    public class NewsAddOrUpdateDto
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTOs
{
    public record GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }
}

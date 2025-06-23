using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTOs
{
    public record CreateTournamentDto
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
    }
}

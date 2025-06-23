using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTOs
{
    public record TournamentDetailsDto
    {
        public int Id { get; set; }
        // Use string.Empty to ensure Title is never null by default
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }



    }
}

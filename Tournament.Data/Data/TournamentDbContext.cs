using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Data.Data
{
    public class TournamentDbContext : DbContext
    {
        public TournamentDbContext (DbContextOptions<TournamentDbContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentDetails> Tournaments { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
    }
}

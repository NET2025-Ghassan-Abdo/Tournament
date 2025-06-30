using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentDbContext _context;

        public TournamentRepository(TournamentDbContext context)
        {
            _context = context;
        }

        public void Add(TournamentDetails tournament)
        {
            _context.Tournaments.Add(tournament);
        }

        public void Delete(TournamentDetails tournament)
        {
            _context.Tournaments.Remove(tournament);
        }

        public async Task<TournamentDetails> GetTournamentAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(bool includeGames = false)
        {
            return includeGames ?     await _context.Tournaments.Include(c => c.Games).ToListAsync()
                                    : await _context.Tournaments.ToListAsync();
        }
    }
}

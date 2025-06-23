using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class TournamentRepository(TournamentDbContext context) : ITournamentRepository
    {
        
        public void Add(TournamentDetails tournament)
        {
            context.Tournaments.Add(tournament);
            context.SaveChanges();
        }

        public async  Task<bool> AnyAsync(int id)
        {
            return await context.Tournaments.AnyAsync(t => t.Id == id);

        }

        public async Task<IEnumerable<TournamentDetails>> GetAllAsync()
        {
            return await context.Tournaments
                .Include(t => t.Games)
                .ToListAsync();
        }

        public Task<TournamentDetails> GetAsync(int id)
        {
            return context.Tournaments
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Remove(int id)
        {
            var tournament = context.Tournaments.Find(id);
            if (tournament != null)
            {
                context.Tournaments.Remove(tournament);
                context.SaveChanges();
            }
        }

        public void Update(TournamentDetails tournament)
        {
            context.Tournaments.Update(tournament);
            context.SaveChanges();
        }
    }
}

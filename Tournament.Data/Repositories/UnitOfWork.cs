using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Interfaces;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TournamentDbContext _context;
        public ITournamentRepository Tournaments { get; }
        public IGameRepository Games { get; }
        public UnitOfWork(TournamentDbContext context, ITournamentRepository tournaments, IGameRepository games)
        {
            _context = context;
            Tournaments = tournaments;
            Games = games ;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

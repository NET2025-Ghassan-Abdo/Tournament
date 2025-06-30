using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<TournamentDetails>> GetTournamentsAsync(bool includeGames = false);
        Task<TournamentDetails> GetTournamentAsync(int id);
        //Task <bool> FindAsync(int id);
        void Add(TournamentDetails tournament);
        //void Update(TournamentDetails tournament);
        void Delete(TournamentDetails tournament);
    }
}

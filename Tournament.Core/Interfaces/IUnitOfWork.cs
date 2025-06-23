using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;

namespace Tournament.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ITournamentRepository Tournaments { get; }
        IGameRepository Games { get; }
        Task<int> SaveChangesAsync();
    }
}

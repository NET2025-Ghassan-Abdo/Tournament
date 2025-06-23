﻿using Microsoft.EntityFrameworkCore;
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
    public class GameRepository(TournamentDbContext _context) : IGameRepository
    {
        public void Add(Game game)
        {
             _context.Game.Add(game);
            _context.SaveChanges();
        }

        public Task<bool> AnyAsync(int id)
        {
            return _context.Game.AnyAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game.ToListAsync();   
        }

        public async Task<Game> GetAsync(int id)
        {
            return await _context.Game.FirstOrDefaultAsync(g => g.Id == id);      
        }

        public void Remove(int id)
        {
            var game = _context.Game.Find(id);
            if (game != null)
            {
                _context.Game.Remove(game);
                _context.SaveChanges();
            }
        }

        public void Update(Game game)
        {
            _context.Game.Update(game);
            _context.SaveChanges();
        }
    }
}

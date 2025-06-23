using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
    // ...

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            var games = await _gameRepository.GetAllAsync();
            var gameDtos = _mapper.Map<IEnumerable<GameDto>>(games);
            return Ok(gameDtos);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null)
                return NotFound();

            var gameDto = _mapper.Map<GameDto>(game);
            return Ok(gameDto);
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public  IActionResult PutGame(int id, GameDto gameDto)
        {
            if (id != gameDto.Id)
                return  BadRequest();

            var game = _mapper.Map<Game>(gameDto);
            _gameRepository.Update(game);
            return NoContent();
        }

        // POST: api/Games
        [HttpPost]
        public ActionResult<GameDto> PostGame(GameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            _gameRepository.Add(game);
            // Optionally map back to DTO if you want to return the created object
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, _mapper.Map<GameDto>(game));
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var exists = await _gameRepository.AnyAsync(id);
            if (!exists)
                return NotFound();

            _gameRepository.Remove(id);
            return NoContent();
        }
    }
}

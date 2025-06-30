using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;
using Tournament.Core.DTOs;
using AutoMapper;
using Tournament.Core.Repositories;
using System.Text.Json.Serialization;
using Tournament.Core.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public TournamentDetailsController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }


        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetailsDto>>> GetTournamentsAsync( bool includeGames)
        {
            var tournaments = includeGames? _mapper.Map<IEnumerable<TournamentDetailsDto>>(await _uow.TournamentRepository.GetTournamentsAsync(true))
                                            : _mapper.Map<IEnumerable<TournamentDetailsDto>>(await _uow.TournamentRepository.GetTournamentsAsync());

           

            return Ok(tournaments);
        }
        // GET: api/TournamentDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TournamentDetailsDto>> GetTournamentAsync(int id)
        {
            TournamentDetails? tournamentDetails = await _uow.TournamentRepository.GetTournamentAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<TournamentDetailsDto>(tournamentDetails);
            return Ok(dto);
        }

        // PUT: api/TournamentDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto dto )
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var existingTournament = await _uow.TournamentRepository.GetTournamentAsync(id);
            if (existingTournament == null)
                return NotFound("Tournament does not exist");
            

            _mapper.Map(dto, existingTournament);
            await _uow.CompleteAsync();

            return NoContent();
        }

        // POST: api/TournamentDetails
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(CreateTournamentDto dto)
        {
            var tournamentDetails = _mapper.Map<TournamentDetails>(dto);
           

            _uow.TournamentRepository.Add(tournamentDetails);
            await _uow.CompleteAsync();

            var createdTournament = _mapper.Map<TournamentDetailsDto>(tournamentDetails);
            return CreatedAtAction(nameof(GetTournamentAsync), new { id = createdTournament.Id }, createdTournament);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournamentDetails = await _uow.TournamentRepository.GetTournamentAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound("Tournament not found");
            }

            _uow.TournamentRepository.Delete(tournamentDetails);

            return NoContent();
        }

       
    }
}

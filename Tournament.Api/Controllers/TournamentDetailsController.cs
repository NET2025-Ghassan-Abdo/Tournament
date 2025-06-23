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

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController(ITournamentRepository repository, IMapper mapper) : ControllerBase
    {
        private readonly ITournamentRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetailsDto>>> GetTournamentDetails()
        {
            var tournaments = await _repository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<TournamentDetailsDto>>(tournaments);
            return Ok(dtoList);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
        {
            var tournamentDetails = await _repository.GetAsync(id);

            if (tournamentDetails == null)
            {
                return NotFound();
            }

            return tournamentDetails;
        }

        // PUT: api/TournamentDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetails tournamentDetails)
        {
           

            return NoContent();
        }

        // POST: api/TournamentDetails
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(CreateTournamentDto dto)
        {
            var tournamentDetails = new TournamentDetails
            {
                Title = dto.Title,
                StartDate = dto.StartDate
            };

            _repository.Add(tournamentDetails);
            // Assuming repository handles SaveChangesAsync internally or via UnitOfWork

            return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            var tournamentDetails = await _repository.GetAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            _repository.Remove(id);

            return NoContent();
        }

        //public async Task<bool> TournamentDetailsExists(int id)
        //{
        //    return await _repository.AnyAsync(id);
        //}
    }
}

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


namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        // GET: api/TournamentDetails
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TournamentDetailsDto>>> GetTournamentDetails(bool includeGames)
        //{
        //    var tournaments = await _repository.GetAllAsync();
        //    var dtoList = _mapper.Map<IEnumerable<TournamentDetailsDto>>(tournaments);
        //    return Ok(dtoList);
        //}

        // GET: api/TournamentDetails/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetailsDto>>> GetTournamentDetails( bool includeGames )
        {
            var tournaments = await _unitOfWork.Tournaments.GetAllAsync();

            IEnumerable<TournamentDetailsDto> dtoList;
            if (includeGames)
            {
                dtoList = _mapper.Map<IEnumerable<TournamentDetailsDto>>(tournaments);
            }
            else
            {
                // Map and set Games to null or empty for each DTO
                dtoList = _mapper.Map<IEnumerable<TournamentDetailsDto>>(tournaments)
                    .Select(dto => dto with { Games = null }); // or Enumerable.Empty<GameDto>()
            }

            return Ok(dtoList);
        }

        // PUT: api/TournamentDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateDto dto )
        {
            var existingTournament = await _unitOfWork.Tournaments.GetAsync(id);
            if (existingTournament == null)
                return NotFound("Tournament does not exist");
            

            _mapper.Map(dto, existingTournament);
            _unitOfWork.Tournaments.Update(existingTournament);

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

            _unitOfWork.Tournaments.Add(tournamentDetails);

            return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            var tournamentDetails = await _unitOfWork.Tournaments.GetAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            _unitOfWork.Tournaments.Remove(id);

            return NoContent();
        }

        //public async Task<bool> TournamentDetailsExists(int id)
        //{
        //    return await _repository.AnyAsync(id);
        //}
    }
}

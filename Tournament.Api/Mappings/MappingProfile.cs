using AutoMapper;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;
namespace Tournament.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TournamentDetails, TournamentDetailsDto>();
            CreateMap<TournamentUpdateDto, TournamentDetails>();
            CreateMap<CreateTournamentDto, TournamentDetails>();
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
        }
    }
    
}


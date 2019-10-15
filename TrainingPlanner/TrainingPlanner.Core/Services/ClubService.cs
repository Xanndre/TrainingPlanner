using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IMapper _mapper;

        public ClubService(IClubRepository clubRepository, IMapper mapper)
        {
            _clubRepository = clubRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClubDTO>> GetAllClubs()
        {
            var clubs = await _clubRepository.GetAllClubs();
            return _mapper.Map<IEnumerable<ClubDTO>>(clubs);
        }

        public async Task<ClubDTO> GetClub(int id)
        {
            var club = await _clubRepository.GetClub(id);
            return _mapper.Map<ClubDTO>(club);
        }

        public async Task<ClubDTO> UpdateClub(ClubDTO club)
        {
            var mappedClub = _mapper.Map<Club>(club);
            var returnedClub = await _clubRepository.UpdateClub(mappedClub);
            return _mapper.Map<ClubDTO>(returnedClub);
        }

        public async Task<ClubCreateDTO> CreateClub(ClubCreateDTO club)
        {
            var mappedClub = _mapper.Map<Club>(club);
            var returnedClub = await _clubRepository.CreateClub(mappedClub);
            return _mapper.Map<ClubCreateDTO>(returnedClub);
        }

        public async Task DeleteClub(int id)
        {

            var club = await _clubRepository.GetClub(id);
            await _clubRepository.DeleteClub(club);
        }
    }
}

using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Stuff;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class SportService : ISportService
    {
        private readonly ISportRepository _sportRepository;
        private readonly IMapper _mapper;

        public SportService(ISportRepository sportRepository, IMapper mapper)
        {
            _sportRepository = sportRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SportDTO>> GetAllSports()
        {
            var sports = await _sportRepository.GetAllSports();
            return _mapper.Map<IEnumerable<SportDTO>>(sports);
        }

        public async Task<IEnumerable<SportDTO>> GetSportsByNames(string sportNames)
        {
            var sports = await _sportRepository.GetSportsByNames(sportNames);
            return _mapper.Map<IEnumerable<SportDTO>>(sports);
        }

        public async Task<SportDTO> GetSport(int id)
        {
            var sport = await _sportRepository.GetSport(id);
            return _mapper.Map<SportDTO>(sport);
        }

        public async Task<SportDTO> UpdateSport(SportDTO sport)
        {
            var mappedSport = _mapper.Map<Sport>(sport);
            var returnedSport = await _sportRepository.UpdateSport(mappedSport);
            return _mapper.Map<SportDTO>(returnedSport);
        }

        public async Task<SportDTO> CreateSport(SportDTO sport)
        {
            var mappedSport = _mapper.Map<Sport>(sport);
            var returnedSport = await _sportRepository.CreateSport(mappedSport);
            return _mapper.Map<SportDTO>(returnedSport);
        }

        public async Task DeleteSport(int id)
        {

            var sport = await _sportRepository.GetSport(id);
            await _sportRepository.DeleteSport(sport);
        }
    }
}

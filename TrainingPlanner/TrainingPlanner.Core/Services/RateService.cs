using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubRate;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class RateService : IRateService
    {
        private const int MaxPageSize = 20;
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;

        public RateService(IRateRepository rateRepository, IMapper mapper)
        {
            _rateRepository = rateRepository;
            _mapper = mapper;
        }

        public async Task<ClubRateDTO> GetClubRate(string userId, int clubId)
        {
            var rate = await _rateRepository.GetClubRate(userId, clubId);
            return _mapper.Map<ClubRateDTO>(rate);
        }

        public async Task<ClubRateDTO> UpdateClubRate(ClubRateDTO rate)
        {
            var mappedRate = _mapper.Map<ClubRate>(rate);
            var returnedRate = await _rateRepository.UpdateClubRate(mappedRate);
            return _mapper.Map<ClubRateDTO>(returnedRate);
        }

        public async Task<ClubRateCreateDTO> CreateClubRate(ClubRateCreateDTO rate)
        {
            var mappedRate = _mapper.Map<ClubRate>(rate);
            var returnedRate = await _rateRepository.CreateClubRate(mappedRate);
            return _mapper.Map<ClubRateCreateDTO>(returnedRate);
        }

        public async Task DeleteClubRate(int id)
        {

            var rate = await _rateRepository.GetClubRateById(id);
            await _rateRepository.DeleteClubRate(rate);
        }

        public async Task<PagedClubRatesDTO> GetAllClubRates(
            int pageNumber,
            int pageSize,
            int clubId)
        {
            var rates = await _rateRepository.GetClubRates(clubId);
            var result = GetClubRates(pageNumber, pageSize, rates);
            return result;
        }

        public async Task<TrainerRateDTO> GetTrainerRate(string userId, int trainerId)
        {
            var rate = await _rateRepository.GetTrainerRate(userId, trainerId);
            return _mapper.Map<TrainerRateDTO>(rate);
        }

        public async Task<TrainerRateDTO> UpdateTrainerRate(TrainerRateDTO rate)
        {
            var mappedRate = _mapper.Map<TrainerRate>(rate);
            var returnedRate = await _rateRepository.UpdateTrainerRate(mappedRate);
            return _mapper.Map<TrainerRateDTO>(returnedRate);
        }

        public async Task<TrainerRateCreateDTO> CreateTrainerRate(TrainerRateCreateDTO rate)
        {
            var mappedRate = _mapper.Map<TrainerRate>(rate);
            var returnedRate = await _rateRepository.CreateTrainerRate(mappedRate);
            return _mapper.Map<TrainerRateCreateDTO>(returnedRate);
        }

        public async Task DeleteTrainerRate(int id)
        {

            var rate = await _rateRepository.GetTrainerRateById(id);
            await _rateRepository.DeleteTrainerRate(rate);
        }

        public async Task<PagedTrainerRatesDTO> GetAllTrainerRates(
            int pageNumber,
            int pageSize,
            int trainerId)
        {
            var rates = await _rateRepository.GetTrainerRates(trainerId);
            var result = GetTrainerRates(pageNumber, pageSize, rates);
            return result;
        }

        private PagedClubRatesDTO GetClubRates(
            int pageNumber, int pageSize, IEnumerable<ClubRate> rates)
        {
            var result = GetPagedClubRates(rates, pageNumber, pageSize);
            return result;
        }

        private PagedClubRatesDTO GetPagedClubRates(IEnumerable<ClubRate> rates, int pageNumber, int pageSize)
        {
            var result = new PagedClubRatesDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedRates = rates
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = rates.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Rates = _mapper.Map<IEnumerable<ClubRateBaseDTO>>(pagedRates);

            return result;
        }

        private PagedTrainerRatesDTO GetTrainerRates(
            int pageNumber, int pageSize, IEnumerable<TrainerRate> rates)
        {
            var result = GetPagedTrainerRates(rates, pageNumber, pageSize);
            return result;
        }

        private PagedTrainerRatesDTO GetPagedTrainerRates(IEnumerable<TrainerRate> rates, int pageNumber, int pageSize)
        {
            var result = new PagedTrainerRatesDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedRates = rates
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = rates.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.Rates = _mapper.Map<IEnumerable<TrainerRateBaseDTO>>(pagedRates);

            return result;
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.BodyMeasurement;
using TrainingPlanner.Core.DTOs.Paged;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class BodyMeasurementService : IBodyMeasurementService
    {
        private const int MaxPageSize = 20;
        private readonly IBodyMeasurementRepository _bodyMeasurementRepository;
        private readonly IMapper _mapper;

        public BodyMeasurementService(IBodyMeasurementRepository bodyMeasurementRepository, IMapper mapper)
        {
            _bodyMeasurementRepository = bodyMeasurementRepository;
            _mapper = mapper;
        }

        public async Task<BodyMeasurementDTO> UpdateBodyMeasurement(BodyMeasurementDTO measurement)
        {
            var mappedMeasurement = _mapper.Map<BodyMeasurement>(measurement);
            var returnedMeasurement = await _bodyMeasurementRepository.UpdateBodyMeasurement(mappedMeasurement);
            return _mapper.Map<BodyMeasurementDTO>(returnedMeasurement);
        }

        public async Task<BodyMeasurementCreateDTO> CreateBodyMeasurement(BodyMeasurementCreateDTO measurement)
        {
            var mappedMeasurement = _mapper.Map<BodyMeasurement>(measurement);
            var returnedMeasurement = await _bodyMeasurementRepository.CreateBodyMeasurement(mappedMeasurement);
            return _mapper.Map<BodyMeasurementCreateDTO>(returnedMeasurement);
        }

        public async Task DeleteBodyMeasurement(int id)
        {

            var measurement = await _bodyMeasurementRepository.GetBodyMeasurement(id);
            await _bodyMeasurementRepository.DeleteBodyMeasurement(measurement);
        }

        public async Task<BodyMeasurementDTO> GetBodyMeasurement(int id)
        {
            var measurement = await _bodyMeasurementRepository.GetBodyMeasurement(id);
            return _mapper.Map<BodyMeasurementDTO>(measurement);
        }

        public async Task<PagedBodyMeasurementsDTO> GetAllBodyMeasurements(
            int pageNumber,
            int pageSize,
            string userId)
        {
            var measurements = await _bodyMeasurementRepository.GetBodyMeasurements(userId);
            var result = GetBodyMeasurements(pageNumber, pageSize, measurements);
            return result;
        }

        private PagedBodyMeasurementsDTO GetBodyMeasurements(
            int pageNumber, int pageSize, IEnumerable<BodyMeasurement> measurements)
        {
            var result = GetPagedBodyMeasurements(measurements, pageNumber, pageSize);
            return result;
        }

        private PagedBodyMeasurementsDTO GetPagedBodyMeasurements(IEnumerable<BodyMeasurement> measurements, int pageNumber, int pageSize)
        {
            var result = new PagedBodyMeasurementsDTO();

            var calculatedPageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
            var pagedMeasurements = measurements
                .Skip(calculatedPageSize * (pageNumber - 1))
                .Take(calculatedPageSize)
                .ToList();

            result.TotalCount = measurements.Count();
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)calculatedPageSize);
            result.BodyMeasurements = _mapper.Map<IEnumerable<BodyMeasurementBaseDTO>>(pagedMeasurements);

            return result;
        }
    }
}

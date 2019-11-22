using AutoMapper;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Reservation;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository repository, IMapper mapper)
        {
            _reservationRepository = repository;
            _mapper = mapper;
        }

        public async Task<ReservationDTO> CreateReservation(ReservationDTO reservation)
        {
            var mappedReservation = _mapper.Map<Reservation>(reservation);
            var res = await _reservationRepository.CreateReservation(mappedReservation);
            return _mapper.Map<ReservationDTO>(res);
        }

        public async Task DeleteReservation(int trainingId, string userId)
        {
            var reservation = await _reservationRepository.GetReservation(trainingId, userId);
            await _reservationRepository.DeleteReservation(reservation);
        }
    }
}

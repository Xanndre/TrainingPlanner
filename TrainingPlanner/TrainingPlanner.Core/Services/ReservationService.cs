using AutoMapper;
using System;
using System.Linq;
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
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainingService _trainingService;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper,
                                    ITrainingRepository trainingRepository, ITrainingService trainingService)
        {
            _reservationRepository = reservationRepository;
            _trainingRepository = trainingRepository;
            _trainingService = trainingService;
            _mapper = mapper;
        }

        public async Task<ReservationDTO> CreateReservation(ReservationDTO reservation)
        {
            var mappedReservation = _mapper.Map<Reservation>(reservation);
            var training = await _trainingRepository.GetTraining(mappedReservation.TrainingId);

            mappedReservation.Date = DateTime.Now;
            if (training.Reservations.Count() >= training.Entries)
            {
                mappedReservation.IsReserveList = true;
            }
            var res = await _reservationRepository.CreateReservation(mappedReservation);

            return _mapper.Map<ReservationDTO>(res);
        }

        public async Task DeleteReservation(int trainingId, string userId)
        {
            var reservation = await _reservationRepository.GetReservation(trainingId, userId);
            await _reservationRepository.DeleteReservation(reservation);
            var training = await _trainingRepository.GetTraining(trainingId);
            await _trainingService.UpdateSignedUpList(training);
        }

    }
}

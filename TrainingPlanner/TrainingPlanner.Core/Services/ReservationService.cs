using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Reservation;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainingService _trainingService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper,
                                    ITrainingRepository trainingRepository, ITrainingService trainingService,
                                    IUserService userService, IEmailService emailService)
        {
            _reservationRepository = reservationRepository;
            _trainingRepository = trainingRepository;
            _trainingService = trainingService;
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
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

            var user = await _userService.GetUser(res.UserId);

            if (res.IsReserveList && user.Notification.ReserveListSignUpConfirmed)
            {
                await SendNotificationSignUpConfirmed(res.Id, res.IsReserveList);                
            }

            if(!res.IsReserveList && user.Notification.SignUpConfirmed)
            {
                await SendNotificationSignUpConfirmed(res.Id, res.IsReserveList);
            }

            return _mapper.Map<ReservationDTO>(res);
        }

        public async Task DeleteReservation(int trainingId, string userId)
        {
            var reservation = await _reservationRepository.GetReservation(trainingId, userId);
            await _reservationRepository.DeleteReservation(reservation);

            var user = await _userService.GetUser(userId);

            if (reservation.IsReserveList && user.Notification.ReserveListSignOutConfirmed)
            {
                await SendNotificationSignOutConfirmed(reservation, reservation.IsReserveList);
            }

            if (!reservation.IsReserveList && user.Notification.SignOutConfirmed)
            {
                await SendNotificationSignOutConfirmed(reservation, reservation.IsReserveList);
            }

            var training = await _trainingRepository.GetTraining(trainingId);
            await _trainingService.UpdateSignedUpList(training);
        }

        public async Task<ReservationInfoDTO> GetReservationInfo(string userId, int trainingId)
        {
            var result = new ReservationInfoDTO();
            var trainings = await _trainingRepository.GetReservedTrainings(userId);
            if (trainings.Any(t => t.Id == trainingId))
            {
                result.IsSignedUp = true;
                var reservation = await _reservationRepository.GetReservation(trainingId, userId);
                result.IsReserveList = reservation.IsReserveList;
            }
            else
            {
                result.IsSignedUp = false;
                result.IsReserveList = false;
            }
            return result;
        }

        public async Task SendNotificationSignUpConfirmed(int reservationId, bool isReserveList)
        {
            var reservation = await _reservationRepository.GetReservationById(reservationId);

            var subject = isReserveList ? "Reserve list sign up confirmation" : "Sign up confirmation";
            var name = reservation.Training.ClubId != null ? reservation.Training.Club.Name : reservation.Training.TrainerName;
            string message;

            if (isReserveList)
            {
                message = "Hello " + reservation.User.FirstName + "!<br/>You've just signed up to reserve list for a training " + reservation.Training.Title + " at " + name + ". It will take place on " + reservation.Training.StartDate.ToString() + " in the room " + reservation.Training.Room + "." + DictionaryResources.Regards;
            }
            else
            {
                message = "Hello " + reservation.User.FirstName + "!<br/>You've just signed up for a training " + reservation.Training.Title + " at " + name + ". It will take place on " + reservation.Training.StartDate.ToString() + " in the room " + reservation.Training.Room + "." + DictionaryResources.Regards;
            }
            
            var emailResult = await _emailService.SendEmail(reservation.User.Email, subject, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }
        }

        public async Task SendNotificationSignOutConfirmed(Reservation reservation, bool isReserveList)
        {       
            var subject = isReserveList ? "Reserve list sign out confirmation" : "Sign out confirmation";
            var name = reservation.Training.ClubId != null ? reservation.Training.Club.Name : reservation.Training.TrainerName;
            string message;

            if (isReserveList)
            {
                message = "Hello " + reservation.User.FirstName + "!<br/>You've just successfully signed out from a reserve list of a training " + reservation.Training.Title + " at " + name + ". It was supposed to take place on " + reservation.Training.StartDate.ToString() + " in the room " + reservation.Training.Room + "." + DictionaryResources.Regards;
            }
            else
            {
                message = "Hello " + reservation.User.FirstName + "!<br/>You've just signed out from a training " + reservation.Training.Title + " at " + name + ". It was supposed to take place on " + reservation.Training.StartDate.ToString() + " in the room " + reservation.Training.Room + "." + DictionaryResources.Regards;
            }

            var emailResult = await _emailService.SendEmail(reservation.User.Email, subject, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }
        }

    }
}

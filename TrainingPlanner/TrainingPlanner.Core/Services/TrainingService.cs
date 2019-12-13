using AutoMapper;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Training;
using TrainingPlanner.Core.Helpers;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Core.Specifications.Extensions;
using TrainingPlanner.Core.Specifications.Filters.TrainingFilters;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Core.Utils;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
   

        public TrainingService(
            ITrainingRepository trainingRepository,
            IMapper mapper,
            IReservationRepository reservationRepository,
            ICardService cardService,
            IEmailService emailService)
        {
            _trainingRepository = trainingRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _cardService = cardService;
            _emailService = emailService;
        }

        public async Task<TrainingDTO> GetTraining(int id)
        {
            var training = await _trainingRepository.GetTraining(id);     
            var mappedTraining = _mapper.Map<TrainingDTO>(training);

            var sortedReservations = training.Reservations.OrderBy(c => c.Date);
            var peopleOnTraining = sortedReservations.Take(training.Entries);
            mappedTraining.EntriesLeft = mappedTraining.Entries - peopleOnTraining.Count();

            return mappedTraining;
        }

        public async Task<TrainingUpdateDTO> UpdateTraining(TrainingUpdateDTO training)
        {
            if (training.StartDate > training.EndDate)
            {
                throw new Exception(DictionaryResources.InvalidDates);
            }
            var mappedTraining = _mapper.Map<Training>(training);
            var returnedTraining = await _trainingRepository.UpdateTraining(mappedTraining);
            var trng = await _trainingRepository.GetTraining(training.Id);
            if(trng.Reservations.Count() != 0)
            {
                await UpdateSignedUpList(trng);
            }
            
            return _mapper.Map<TrainingUpdateDTO>(returnedTraining);
        }

        public async Task<TrainingCreateDTO> CreateTraining(TrainingCreateDTO training)
        {
            if(training.StartDate > training.EndDate)
            {
                throw new Exception(DictionaryResources.InvalidDates);
            }
            var mappedTraining = _mapper.Map<Training>(training);
            var returnedTraining = await _trainingRepository.CreateTraining(mappedTraining);

            BackgroundJob.Schedule(() => _cardService.DeleteCardEntries(returnedTraining.Id, returnedTraining.TrainerId, returnedTraining.ClubId), returnedTraining.EndDate);

            BackgroundJob.Schedule(() => SendNotificationIncomingTraining(returnedTraining), returnedTraining.StartDate
                .AddHours(-3));
            
            return _mapper.Map<TrainingCreateDTO>(returnedTraining);
        }

        public async Task DeleteTraining(int id)
        {
            var training = await _trainingRepository.GetTraining(id);
            var reservations = await _reservationRepository.GetReservationsOnTraining(id);

            await _trainingRepository.DeleteTraining(training);

            if(reservations != null)
            {
                foreach (var reservation in reservations)
                {
                    if (reservation.User.Notification.TrainingDeleted)
                    {
                        await SendNotificationTrainingDeleted(reservation, training);
                    }
                }
            }
            
        }

        public async Task<IEnumerable<TrainingDTO>> GetTrainerTrainings(int trainerId, TrainingFilterData filterData)
        {
            var trainings = await _trainingRepository.GetTrainerTrainings(trainerId);
            trainings = Filter(filterData, trainings);
            return _mapper.Map<IEnumerable<TrainingDTO>>(trainings);
        }

        public async Task<IEnumerable<TrainingDTO>> GetClubTrainings(int clubId, TrainingFilterData filterData)
        {
            var trainings = await _trainingRepository.GetClubTrainings(clubId);
            trainings = Filter(filterData, trainings);
            return _mapper.Map<IEnumerable<TrainingDTO>>(trainings);
        }

        public async Task<IEnumerable<TrainingDTO>> GetReservedTrainings(string userId, TrainingFilterData filterData)
        {
            var trainings = await _trainingRepository.GetReservedTrainings(userId);
            trainings = Filter(filterData, trainings);
            return _mapper.Map<IEnumerable<TrainingDTO>>(trainings);
        }

        public async Task UpdateSignedUpList(Training training)
        {
            var sortedReservations = training.Reservations.OrderBy(c => c.Date);
            var reservationsBeforeUpdate = _reservationRepository.GetReservationsOnTraining(training.Id).Result
                .OrderBy(c => c.Date)
                .ToList();

            var peopleOnBench = sortedReservations.Skip(training.Entries);
            var peopleOnTraining = sortedReservations.Take(training.Entries);

            foreach (var person in peopleOnBench)
            {
                person.IsReserveList = true;
            }

            foreach (var person in peopleOnTraining)
            {
                person.IsReserveList = false;
            }

            await _reservationRepository.UpdateRange(peopleOnBench);
            await _reservationRepository.UpdateRange(peopleOnTraining);

            
            var reservationsAfterUpdate = training.Reservations.OrderBy(c => c.Date).ToList();
            for(int i = 0; i < reservationsBeforeUpdate.Count(); i++)
            {
                if(reservationsBeforeUpdate[i].IsReserveList != reservationsAfterUpdate[i].IsReserveList)
                {
                    if(reservationsBeforeUpdate[i].User.Notification.ListToReserveList && 
                        reservationsBeforeUpdate[i].IsReserveList == false)
                    {
                        await SendNotificationListToReserveList(reservationsBeforeUpdate[i], training);
                    }
                    else if(reservationsBeforeUpdate[i].User.Notification.ReserveListToList &&
                        reservationsBeforeUpdate[i].IsReserveList == true)
                    {
                        await SendNotificationListToReserveList(reservationsBeforeUpdate[i], training);
                    }
                }
            }
        }

        public async Task<IEnumerable<TrainingCreateDTO>> CreateTrainingRange(IEnumerable<TrainingCreateDTO> trainings)
        {
            foreach (var training in trainings)
            {
                if (training.StartDate > training.EndDate)
                {
                    throw new Exception(DictionaryResources.InvalidDates);
                }
            }
            
            var mappedTrainings = _mapper.Map<IEnumerable<Training>>(trainings);
            var returnedTrainings = await _trainingRepository.CreateTrainingRange(mappedTrainings);
            foreach (var trng in returnedTrainings)
            {
                BackgroundJob.Schedule(() => _cardService.DeleteCardEntries(trng.Id, trng.TrainerId, trng.ClubId), trng.EndDate);
                BackgroundJob.Schedule(() => SendNotificationIncomingTraining(trng), trng.StartDate
                .AddHours(-3));
            }
            return _mapper.Map<IEnumerable<TrainingCreateDTO>>(returnedTrainings);
        }

        public async Task SendNotificationIncomingTraining(Training training)
        {
            var reservations = await _reservationRepository.GetReservationsOnTraining(training.Id);

            if (reservations != null)
            {
                foreach (var reservation in reservations)
                {
                    if (reservation.User.Notification.IncomingTraining)
                    {
                        var subject = "Incoming training";
                        var name = training.ClubId != null ? training.Club.Name : training.TrainerName;
                        var message = "Hello " + reservation.User.FirstName + "!<br/>Training " + training.Title + " at " + name + " is coming up. It will take place on " + training.StartDate.ToString() + " in the room " + training.Room + "." + DictionaryResources.Regards;

                        var emailResult = await _emailService.SendEmail(reservation.User.Email, subject, message);

                        if (emailResult == null)
                        {
                            throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
                        }
                    }
                }
            }

        }

        public async Task SendNotificationTrainingDeleted(Reservation reservation, Training training)
        {
            var subject = "Deleted training";
            var name = training.ClubId != null ? training.Club.Name : training.TrainerName;
            var message = "Hello " + reservation.User.FirstName + "!<br/>Please be informed that training " + training.Title + " at " + name + " has been deleted. It was supposed to take place on " + training.StartDate.ToString() + " in the room " + training.Room + "." + DictionaryResources.Regards;

            var emailResult = await _emailService.SendEmail(reservation.User.Email, subject, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }

        }

        public async Task SendNotificationListToReserveList(Reservation reservation, Training training)
        {
            var subject = reservation.IsReserveList ? "Transfer from reserve list to training list" : "Transfer from training list to reserve list";
            var name = training.ClubId != null ? training.Club.Name : training.TrainerName;
            string message;

            if (reservation.IsReserveList)
            {
                message = "Hello " + reservation.User.FirstName + "!<br/>Please be informed that you've been transfered from reserve list to training list on training " + training.Title + " at " + name + ". It has probably happened due to increased number of training's entries or another user's sign-out. Training will take place on " + training.StartDate.ToString() + " in the room " + training.Room + "." + DictionaryResources.Regards;
            }
            else
            {
                message = "Hello " + reservation.User.FirstName + "!<br/>Please be informed that you've been transfered from training list to reserve list on training " + training.Title + " at " + name + ". It has probably happened due to decreased number of training's entries. Training will take place on " + training.StartDate.ToString() + " in the room " + training.Room + "." + DictionaryResources.Regards;
            }

            var emailResult = await _emailService.SendEmail(reservation.User.Email, subject, message);

            if (emailResult == null)
            {
                throw new ApplicationException(DictionaryResources.InvalidSendAttempt);
            }

        }

        private IEnumerable<Training> Filter(TrainingFilterData filterData, IEnumerable<Training> trainings)
        {
            trainings = trainings.Where(training => ApplyFilters(filterData)
                .IsSatisfiedBy(training))
                .ToList();
            return trainings;
        }

        private ISpecification<Training> ApplyFilters(TrainingFilterData filterData)
        {
            return new TrainingMatchesLevel(filterData.Level)
                .And(new TrainingMatchesTitle(filterData.Title))
                .And(new TrainingHoursInRange(filterData.DateLowerBound, filterData.DateUpperBound));
        }

    }
}

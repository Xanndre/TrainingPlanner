using AutoMapper;
using System.Linq;
using TrainingPlanner.Core.DTOs.Account;
using TrainingPlanner.Core.DTOs.BodyMeasurement;
using TrainingPlanner.Core.DTOs.Club;
using TrainingPlanner.Core.DTOs.ClubStuff;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubCard;
using TrainingPlanner.Core.DTOs.ClubStuff.ClubRate;
using TrainingPlanner.Core.DTOs.Favourite;
using TrainingPlanner.Core.DTOs.Stuff;
using TrainingPlanner.Core.DTOs.Trainer;
using TrainingPlanner.Core.DTOs.TrainerStuff;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerCard;
using TrainingPlanner.Core.DTOs.TrainerStuff.TrainerRate;
using TrainingPlanner.Core.DTOs.User;
using TrainingPlanner.Core.DTOs.UserStuff;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>()
                .ForMember(c => c.UserName, d => d.MapFrom(e => e.Email))
                .ForMember(c => c.BirthDate, d => d.MapFrom(e => e.BirthDate.ToLocalTime().Date))
                .ReverseMap();

            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(c => c.BirthDate, d => d.MapFrom(e => e.BirthDate.ToLocalTime().Date))
                .ReverseMap();

            CreateMap<ExternalLoginDTO, ApplicationUser>()
                .ForMember(c => c.UserName, d => d.MapFrom(e => e.Email));

            CreateMap<SportDTO, Sport>().ReverseMap();
            CreateMap<ApplicationUser, PartnerDTO>();

            CreateMap<TrainerPrice, TrainerPriceDTO>()
                .ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<ClubPrice, ClubPriceDTO>()
                .ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<TrainerDTO, Trainer>().ReverseMap();
            CreateMap<ClubDTO, Club>().ReverseMap();

            CreateMap<Trainer, TrainerCreateDTO>().ReverseMap();
            CreateMap<Trainer, TrainerUpdateDTO>().ReverseMap();
            CreateMap<Club, ClubUpdateDTO>().ReverseMap();
            CreateMap<Club, ClubCreateDTO>().ReverseMap();

            CreateMap<TrainerSportDTO, TrainerSport>().ReverseMap();

            CreateMap<UserSport, UserSportDTO>().ReverseMap();
            CreateMap<UserLocation, UserLocationDTO>().ReverseMap();
            CreateMap<BodyInjury, BodyInjuryDTO>().ReverseMap();

            CreateMap<ClubActivity, ClubActivityDTO>()
                .ForMember(c => c.Picture, d => d.MapFrom(e => e.Picture))
                .ReverseMap();

            CreateMap<ClubTrainerDTO, ClubTrainer>().ReverseMap();

            CreateMap<ClubWorkingHoursDTO, ClubWorkingHours>().ReverseMap(); 
                
            CreateMap<PictureDTO, Picture>().ReverseMap();

            CreateMap<Club, ClubBaseDTO>()
            .ForMember(c => c.Picture, d => d.MapFrom(e => e.Pictures.FirstOrDefault(p => p.IsMiniature)))
            .ForMember(c => c.IsFavourite, d => d.MapFrom(e => e.Favourites.Any()));

            CreateMap<Trainer, TrainerBaseDTO>()
            .ForMember(c => c.IsFavourite, d => d.MapFrom(e => e.Favourites.Any()));

            CreateMap<FavouriteClub, FavouriteClubDTO>().ReverseMap();
            CreateMap<FavouriteTrainer, FavouriteTrainerDTO>().ReverseMap();

            CreateMap<ClubRate, ClubRateBaseDTO>();
            CreateMap<TrainerRate, TrainerRateBaseDTO>();
            CreateMap<BodyMeasurement, BodyMeasurementBaseDTO>();
            CreateMap<ClubRate, ClubRateDTO>().ReverseMap();
            CreateMap<TrainerRate, TrainerRateDTO>().ReverseMap();
            CreateMap<BodyMeasurement, BodyMeasurementDTO>().ReverseMap();
            CreateMap<ClubRate, ClubRateCreateDTO>().ReverseMap();
            CreateMap<TrainerRate, TrainerRateCreateDTO>().ReverseMap();
            CreateMap<BodyMeasurement, BodyMeasurementCreateDTO>().ReverseMap();

            CreateMap<ClubCard, ClubCardBaseDTO>()
                .ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true));

            CreateMap<TrainerCard, TrainerCardBaseDTO>()
                .ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true));

            CreateMap<ClubCard, ClubCardDTO>()
                .ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<TrainerCard, TrainerCardDTO>()
                .ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<ClubCard, ClubCardCreateDTO>().ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<TrainerCard, TrainerCardCreateDTO>().ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<ClubCard, ClubCardUpdateDTO>().ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();

            CreateMap<TrainerCard, TrainerCardUpdateDTO>().ForMember(c => c.UnlimitedEntries, d => d.MapFrom(e => e.Entries != 0 ? false : true))
                .ForMember(c => c.UnlimitedValidityPeriod, d => d.MapFrom(e => e.ValidityPeriod != 0 ? false : true))
                .ReverseMap();
        }

    }
}

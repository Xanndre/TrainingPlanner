﻿using AutoMapper;
using System.Linq;
using TrainingPlanner.Core.DTOs.Account;
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

            CreateMap<TrainerPriceDTO, TrainerPrice>().ReverseMap();
            CreateMap<ClubPriceDTO, ClubPrice>().ReverseMap();

            CreateMap<TrainerDTO, Trainer>().ReverseMap();
            CreateMap<ClubDTO, Club>().ReverseMap();

            CreateMap<Trainer, TrainerCreateDTO>().ReverseMap();
            CreateMap<Trainer, TrainerUpdateDTO>().ReverseMap();
            CreateMap<Club, ClubUpdateDTO>().ReverseMap();
            CreateMap<Club, ClubCreateDTO>().ReverseMap();

            CreateMap<TrainerSportDTO, TrainerSport>().ReverseMap();
            CreateMap<TrainerSport, TrainerSportBaseDTO>().ReverseMap();

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
            CreateMap<ClubRate, ClubRateDTO>().ReverseMap();
            CreateMap<TrainerRate, TrainerRateDTO>().ReverseMap();
            CreateMap<ClubRate, ClubRateCreateDTO>().ReverseMap();
            CreateMap<TrainerRate, TrainerRateCreateDTO>().ReverseMap();

            CreateMap<ClubCard, ClubCardBaseDTO>();
            CreateMap<TrainerCard, TrainerCardBaseDTO>();
            CreateMap<ClubCard, ClubCardDTO>().ReverseMap();
            CreateMap<TrainerCard, TrainerCardDTO>().ReverseMap();
            CreateMap<ClubCard, ClubCardCreateDTO>().ReverseMap();
            CreateMap<TrainerCard, TrainerCardCreateDTO>().ReverseMap();
            CreateMap<ClubCard, ClubCardUpdateDTO>().ReverseMap();
            CreateMap<TrainerCard, TrainerCardUpdateDTO>().ReverseMap();
        }

    }
}

using AutoMapper;
using System.Linq;
using TrainingPlanner.Core.DTOs;
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

            CreateMap<TrainerPriceBasicDTO, TrainerPrice>().ReverseMap();
            CreateMap<ClubPriceBasicDTO, ClubPrice>().ReverseMap();

            CreateMap<TrainerDTO, Trainer>().ReverseMap();
            CreateMap<ClubDTO, Club>().ReverseMap();

            CreateMap<Trainer, TrainerCreateDTO>().ReverseMap();
            CreateMap<Trainer, TrainerUpdateDTO>().ReverseMap();
            CreateMap<Club, ClubCreateDTO>()
                .ForMember(c => c.Activities, d=> d.MapFrom(e => e.Activities))
                .ReverseMap();

            CreateMap<TrainerSportDTO, TrainerSport>().ReverseMap();
            CreateMap<TrainerSport, TrainerSportBasicDTO>();
            CreateMap<TrainerSportBasicDTO, TrainerSport>();

            CreateMap<ClubActivity, ClubActivityBasicDTO>()
                .ForMember(c => c.Picture, d => d.MapFrom(e => e.Picture))
                .ReverseMap();

            CreateMap<ClubTrainerBasicDTO, ClubTrainer>().ReverseMap();

            CreateMap<ClubWorkingHoursBasicDTO, ClubWorkingHours>().ReverseMap();
            CreateMap<PictureDTO, Picture>().ReverseMap();

            CreateMap<Club, ClubBaseDTO>()
            .ForMember(c => c.Picture, d => d.MapFrom(e => e.Pictures.FirstOrDefault(p => p.IsMiniature)))
            .ForMember(c => c.IsFavourite, d => d.MapFrom(e => e.Favourites.Any()));

            CreateMap<Trainer, TrainerBaseDTO>()
            .ForMember(c => c.IsFavourite, d => d.MapFrom(e => e.Favourites.Any()));

        }

    }
}

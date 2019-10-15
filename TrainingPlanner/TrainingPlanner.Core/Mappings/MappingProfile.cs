using AutoMapper;
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
            CreateMap<Club, ClubCreateDTO>().ReverseMap();

            CreateMap<TrainerSportDTO, TrainerSport>().ReverseMap();
            CreateMap<TrainerSport, TrainerSportBasicDTO>();
            CreateMap<TrainerSportBasicDTO, TrainerSport>();

        }

    }
}

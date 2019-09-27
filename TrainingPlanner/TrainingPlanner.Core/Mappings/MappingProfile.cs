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
                .ReverseMap();

            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(c => c.BirthDate, d => d.MapFrom(e => e.BirthDate.ToLocalTime().Date))
                .ReverseMap();

            CreateMap<ExternalLoginDTO, ApplicationUser>()
                .ForMember(c => c.UserName, d => d.MapFrom(e => e.Email));
        }

    }
}

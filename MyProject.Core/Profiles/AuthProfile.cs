using AutoMapper;
using MyProject.Core.Dtos.RequestDtos;
using MyProject.Core.Dtos.ResponseDtos;
using MyProject.Core.Models;

namespace MyProject.Core.Profiles;

public class AuthProfile : Profile
{
    // public AuthProfile()
    // {
    //     CreateMap<User, UserResponseDto>()
    //         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    //         .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
    //         
    //     CreateMap<UserRegisterRequestDto, User>()
    //         .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
    // }
}
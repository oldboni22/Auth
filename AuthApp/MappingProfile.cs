using AutoMapper;
using Domain.Entities;
using Shared.Models;

namespace AuthApp;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreateDto, User>();
    }
}
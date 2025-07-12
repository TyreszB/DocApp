using System;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, User>();
        CreateMap<Aircraft, Aircraft>();
        CreateMap<Discrepency, Discrepency>();
    }

}

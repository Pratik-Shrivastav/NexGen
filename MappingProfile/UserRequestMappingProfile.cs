using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NexGen.Model;
using NextGen.Request;

namespace NextGen.MappingProfile
{
    public class UserRequestMappingProfile: Profile
    {
        public UserRequestMappingProfile()
        {
            CreateMap<UserRequest, User>().ReverseMap();
        }
    }
}
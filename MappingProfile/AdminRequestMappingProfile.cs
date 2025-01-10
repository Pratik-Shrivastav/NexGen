using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NexGen.Model;
using NextGen.Request;

namespace NextGen.MappingProfile
{
    public class AdminRequestMappingProfile : Profile
    {
        public AdminRequestMappingProfile()
        {
            CreateMap<AdminRequest, Admin>().ReverseMap();
        }
    }
}
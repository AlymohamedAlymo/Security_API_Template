using API_Template.Data.Data.DTOs;
using AutoMapper;
using Security_API_Template.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Template.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AppUsers, MemberDTO>();
            CreateMap<Photo, PhotoDTO>();
        }
    }
}

using API_Template.Data.Data.DTOs;
using AutoMapper;
using Security_API_Template.Data.Entites;
using Security_API_Template.Extensions;

namespace API_Template.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AppUsers, MemberDTO>()
                .ForMember(m =>m.Age, f => f.MapFrom(x => x.DateOfBirth.CalculateAge()))
                .ForMember(m => m.PhotoUrl, f => f.MapFrom(u => u.Photos.FirstOrDefault(x => x.IsMain)!.Url));

            CreateMap<Photo, PhotoDTO>();
        }
    }
}

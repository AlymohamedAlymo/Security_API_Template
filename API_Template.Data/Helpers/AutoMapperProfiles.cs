using API_Template.Data.ViewModels;
using AutoMapper;
using API_Template.Data.DataBase.Entites;

namespace API_Template.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AppUsers, MemberDto>()
                .ForMember(m =>m.Age, f => f.MapFrom(x => x.DateOfBirth.CalculateAge()))
                .ForMember(m => m.PhotoUrl, f => f.MapFrom(u => u.Photos.FirstOrDefault(x => x.IsMain)!.Url));

            CreateMap<Photo, PhotoVM>();
            CreateMap<MemberUpdateVM, AppUsers>();
        }
    }
}

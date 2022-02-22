using AutoMapper;
using LawEnforcementApi.Model;
using LawEnforcementApi.Model.DTO;

namespace LawEnforcementApi.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateLawEnforcement, LawEnforcement>();
        }
    }
}

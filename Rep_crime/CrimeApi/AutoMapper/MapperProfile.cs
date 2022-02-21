using AutoMapper;
using CrimeApi.Models;
using CrimeApi.Models.DTO;

namespace CrimeApi.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCrimeEvent, CrimeEvent>();
        }
    }
}

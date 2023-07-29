using AutoMapper;
using Course_Backend.Models.DTO;
using Course_Backend.Models;

namespace Course_Backend.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Students, StudentsCreateDTO>().ReverseMap();
            
        }
    }
}

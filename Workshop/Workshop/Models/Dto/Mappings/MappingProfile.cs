using AutoMapper;
using Workshop.Models.Dto.Responses;


namespace Workshop.Models.Dto.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, RegisterResponse>();
            CreateMap<BorrowInfo, BorrowResponse>();
        }
    }
}
using AutoMapper;
using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mapping
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<CreditCardEntity, CreditCardDTO>().ReverseMap();
        }
    }
}

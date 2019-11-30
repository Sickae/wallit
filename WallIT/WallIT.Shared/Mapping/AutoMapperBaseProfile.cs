using AutoMapper;
using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;

namespace WallIT.Shared.Mapping
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<CreditCardEntity, CreditCardDTO>().ReverseMap();
        }
    }
}

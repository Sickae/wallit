using AutoMapper;
using System;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Identity;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mapping
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<CreditCardEntity, CreditCardDTO>().ReverseMap();

            CreateMap<UserEntity, UserDTO>();
            CreateMap<UserDTO, UserEntity>()
                .ForMember(dest => dest.UserName, m => m.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, m => m.MapFrom(src => src.Email.ToUpper()));

            CreateMap<UserDTO, AppIdentityUser>()
                .ForMember(dest => dest.LockoutEnd, m => m.MapFrom(src => src.LockoutEnd))
                .ForMember(dest => dest.UserName, m => m.MapFrom(src => src.Email))
                .ForMember(dest => dest.NormalizedUserName, m => m.MapFrom(src => src.Email.ToUpper()));
            CreateMap<AppIdentityUser, UserDTO>()
                .ForMember(dest => dest.LockoutEnd, m => m.MapFrom(src => src.LockoutEnd.HasValue ? (DateTime?)DateTime.Parse(src.LockoutEnd.ToString()) : null));

            CreateMap<UserEntity, AppIdentityUser>()
                .ForMember(dest => dest.LockoutEnd, m => m.MapFrom(src => src.LockoutEnd));
            CreateMap<AppIdentityUser, UserEntity>()
                .ForMember(dest => dest.LockoutEnd, m => m.MapFrom(src => src.LockoutEnd.HasValue ? (DateTime?)DateTime.Parse(src.LockoutEnd.ToString()) : null));

            CreateMap<UserClaimEntity, UserClaimDTO>()
                .ForMember(dest => dest.UserId, m => m.MapFrom(src => src.User != null ? src.User.Id : (int?)null));
            CreateMap<UserClaimDTO, UserClaimEntity>()
                .ForMember(dest => dest.User, m => m.MapFrom(src => src.UserId.HasValue ?  new UserEntity { Id = src.UserId.Value } : null));

            CreateMap<UserClaimDTO, AppIdentityUserClaim>().ReverseMap();

            CreateMap<RecordEntity, RecordDTO>().ReverseMap();
        }
    }
}

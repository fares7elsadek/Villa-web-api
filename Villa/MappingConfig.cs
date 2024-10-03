using AutoMapper;
using VillaApp.Models;
using VillaApp.Models.Dtos.UserDto;
using VillaApp.Models.Dtos.VillaDto;
using VillaApp.Models.Dtos.VillaNumberDto;

namespace VillaApp
{
	public class MappingConfig: Profile
	{
        public MappingConfig()
        {
            CreateMap<Villas,VillaDto>().ReverseMap();
            CreateMap<VillaNumber,VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber,VillaNumberUpdateDto>().ReverseMap();
            CreateMap<VillaNumber,VillaNumberCreateDto>().ReverseMap();
            CreateMap<User,UserLoginDto>().ReverseMap();
            CreateMap<User,UserRegisterationDto>().ReverseMap();
            CreateMap<User,RegisterResponseDto>().ReverseMap();
        }
    }
}

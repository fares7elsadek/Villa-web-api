using AutoMapper;
using VillaApp.Models;
using VillaApp.Models.Dtos.VillaDto;

namespace VillaApp
{
	public class MappingConfig: Profile
	{
        public MappingConfig()
        {
            CreateMap<Villas,VillaDto>().ReverseMap();
        }
    }
}

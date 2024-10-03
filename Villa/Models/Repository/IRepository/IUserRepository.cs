using VillaApp.Models.Dtos.UserDto;

namespace VillaApp.Models.Repository.IRepository
{
	public interface IUserRepository: IRepository<User>
	{
		void Update (User user);
		bool IsUnique(string  username);
		Task<LoginResponseDto> LoginAsync(UserLoginDto userLoginDto);
		Task<RegisterResponseDto> RegisterAsync(UserRegisterationDto userRegisterationDto);
	}
}

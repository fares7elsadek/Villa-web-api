using Azure.Identity;

namespace VillaApp.Models.Dtos.UserDto
{
	public class LoginResponseDto
	{
		public string Token { get; set; }

		public string UserName { get; set; }

        public string Name { get; set; }

		public Guid Id { get; set; }
	}
}

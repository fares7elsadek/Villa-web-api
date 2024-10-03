namespace VillaApp.Models.Dtos.UserDto
{
	public class RegisterResponseDto
	{
		public Guid Id { get; set; }

		public string UserName { get; set; }

		public string Name { get; set; }

		public string Role { get; set; }
	}
}

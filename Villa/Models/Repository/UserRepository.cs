using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaApp.Models.Data;
using VillaApp.Models.Dtos.UserDto;
using VillaApp.Models.Repository.IRepository;
using VillaApp.Options;

namespace VillaApp.Models.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly AppDbContext _db;
		private readonly JwtOptions _jwtOptions;
		private readonly IMapper _mapper;
       
        public UserRepository(AppDbContext db
			, JwtOptions jwtOptions,
			IMapper mapper) : base(db)
		{
			_db = db;
			_jwtOptions = jwtOptions;
			_mapper = mapper;
		}

		public bool IsUnique(string username)
		{
			var user = _db.users.FirstOrDefault(u => u.UserName == username);
			if (user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDto> LoginAsync(UserLoginDto userLoginDto)
		{
			var user =  await _db.users.FirstOrDefaultAsync(u => u.UserName == userLoginDto.UserName
			&& u.Password == userLoginDto.Password);
			if (user == null)
			{
				return null;
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = _jwtOptions.issuer,
				Audience = _jwtOptions.audience,
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.signkey)),
					SecurityAlgorithms.HmacSha256),
				Subject = new ClaimsIdentity(new Claim[]
				{
					new(ClaimTypes.NameIdentifier,user.Id.ToString()),
					new(ClaimTypes.Name,user.UserName),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			var response = new LoginResponseDto
			{
				Token = tokenHandler.WriteToken(token),
				UserName = userLoginDto.UserName,
				Name = user.Name,
				Id = user.Id,
			};

			return response;
		}

		public async Task<RegisterResponseDto> RegisterAsync(UserRegisterationDto userRegisterationDto)
		{
			User newUser = _mapper.Map<User>(userRegisterationDto);
			await _db.users.AddAsync(newUser);
			await _db.SaveChangesAsync();
			return _mapper.Map<RegisterResponseDto>(newUser);
		}

		public void Update(User user)
		{
			_db.users.Update(user);
		}
	}
}

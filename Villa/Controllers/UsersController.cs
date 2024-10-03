using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaApp.Models.Repository.IRepository;
using VillaApp.Models;
using VillaApp.Models.Dtos.UserDto;
using System.Net;
using System.Runtime.CompilerServices;

namespace VillaApp.Controllers
{
	[Route("api/User")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private ApiResponse _response;
		public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_response = new ApiResponse();
		}

		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> Login([FromBody] UserLoginDto loginDto)
		{
			try
			{
				var UserResponse = await _unitOfWork.User.LoginAsync(loginDto);
				if(UserResponse == null || string.IsNullOrEmpty(UserResponse.Token))
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					_response.Errors.Add("Username or password is incorrect");
					return BadRequest(_response);
				}
				_response.StatusCode =HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.Result = UserResponse;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, _response);
			}
		}
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> Register([FromBody] UserRegisterationDto userRegisteration)
		{
			try
			{
				var isUnique = _unitOfWork.User.IsUnique(userRegisteration.UserName);
				if (!isUnique)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					_response.Errors.Add("Username is Already exsit");
					return BadRequest(_response);
				}
				var user = await _unitOfWork.User.RegisterAsync(userRegisteration);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				_response.Result = user;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, _response);
			}
		}

	}
}

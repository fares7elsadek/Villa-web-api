using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaApp.Models.Repository.IRepository;
using VillaApp.Models;
using System.Net;
using VillaApp.Models.Dtos.VillaDto;
using VillaApp.Models.Dtos.VillaNumberDto;
using Microsoft.AspNetCore.JsonPatch;

namespace VillaApp.Controllers
{
	[Route("api/VillaNumber")]
	[ApiController]
	public class VillaNumberController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private ApiResponse _response;
		public VillaNumberController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_response = new ApiResponse();
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> GetVillasNumber()
		{
			try
			{
				var villas = await _unitOfWork.VillaNumber.GetAllAsync();
				if (villas == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				_response.Result = _mapper.Map<IEnumerable<VillaNumberDto>>(villas);
				_response.StatusCode = HttpStatusCode.OK;
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

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> GetVillaNumber(Guid id)
		{
			try
			{
				var villa = await _unitOfWork.VillaNumber.GetOrDefaultAsync(v => v.Id == id);
				if (villa == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				_response.Result = _mapper.Map<VillaNumberDto>(villa);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
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

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> AddVilla([FromBody] VillaNumberCreateDto villaNumber)
		{
			try
			{
				if (villaNumber == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				var newId = Guid.NewGuid();
				var newVilla = _mapper.Map<VillaNumber>(villaNumber);
				newVilla.Id = newId;
				await _unitOfWork.VillaNumber.AddAsync(newVilla);
				await _unitOfWork.SaveAsync();
				_response.StatusCode = HttpStatusCode.Created;
				_response.IsSuccess = true;
				_response.Result = newVilla;
				var uri = Url.Action("GetVillaNumber", new { id = newVilla.Id });
				return Created(uri, _response);
			}
			catch (Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, _response);
			}

		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> DeleteVilla(Guid id)
		{
			try
			{
				var villa = await _unitOfWork.VillaNumber.GetOrDefaultAsync(villa => villa.Id == id);
				if (villa == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				_unitOfWork.VillaNumber.Delete(villa);
				await _unitOfWork.SaveAsync();
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
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
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> Update([FromBody] VillaNumberUpdateDto villaNumber)
		{
			try
			{
				var oldvilla = await _unitOfWork.VillaNumber.GetOrDefaultAsync(v => v.Id == villaNumber.Id);
				if (oldvilla == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				oldvilla = _mapper.Map<VillaNumber>(villaNumber);
				_unitOfWork.VillaNumber.Update(oldvilla);
				await _unitOfWork.SaveAsync();
				_response.StatusCode = HttpStatusCode.Created;
				_response.IsSuccess = true;
				_response.Result = _mapper.Map<VillaNumberDto>(oldvilla);
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

		[HttpPatch("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> UpdatePatch(Guid id, [FromBody] JsonPatchDocument patch)
		{
			try
			{
				var oldvilla = await _unitOfWork.VillaNumber.GetOrDefaultAsync(v => v.Id == id);
				if (oldvilla == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				patch.ApplyTo(oldvilla);
				_unitOfWork.VillaNumber.Update(oldvilla);
				await _unitOfWork.SaveAsync();
				_response.IsSuccess = true;
				_response.StatusCode = HttpStatusCode.OK;
				_response.Result = _mapper.Map<VillaNumberDto>(oldvilla);
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

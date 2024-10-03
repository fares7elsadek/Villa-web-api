using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaApp.Models;
using VillaApp.Models.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using VillaApp.Models.Dtos.VillaDto;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Authorization;


namespace VillaApp.Controllers
{
    [Route("api/Villa")]
	[ApiController]
	public class VillaController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private ApiResponse _response;
        public VillaController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
			_response = new ApiResponse();
        }

        [HttpGet]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ResponseCache(Duration = 30)]
		public async Task<ActionResult<ApiResponse>> GetVillas()
		{
			try
			{
				var villas = await _unitOfWork.Villas.GetAllAsync();
				if (villas == null){
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				_response.Result = _mapper.Map<IEnumerable<VillaDto>>(villas);
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError,_response);
				
			}
			
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> GetVilla(Guid id)
		{
			try
			{
				var villa = await _unitOfWork.Villas.GetOrDefaultAsync(v => v.Id == id);
				if (villa == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				_response.Result = _mapper.Map<VillaDto>(villa);
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch(Exception ex)
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
		public async Task<ActionResult<ApiResponse>> AddVilla([FromBody] VillaDto villa)
		{
			try
			{
				if (villa == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				var newId = Guid.NewGuid();
				var newVilla = _mapper.Map<Villas>(villa);
				newVilla.Id = newId;
				await _unitOfWork.Villas.AddAsync(newVilla);
				await _unitOfWork.SaveAsync();
				_response.StatusCode = HttpStatusCode.Created;
				_response.IsSuccess = true;
				_response.Result = newVilla;
				var uri = Url.Action("GetVilla", new { id = newVilla.Id });
				return Created(uri,_response);
			}
			catch(Exception ex)
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
				var villa = await _unitOfWork.Villas.GetOrDefaultAsync(villa => villa.Id == id);
				if (villa == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				_unitOfWork.Villas.Delete(villa);
				await _unitOfWork.SaveAsync();
				_response.StatusCode = HttpStatusCode.OK;
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch(Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, _response);
			}
			
		}
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ApiResponse>> Update(Guid id,[FromBody]VillaDto villa)
		{
			try
			{
				var oldvilla = await _unitOfWork.Villas.GetOrDefaultAsync(v => v.Id == id);
				if (oldvilla == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				oldvilla = _mapper.Map<Villas>(villa);
				_unitOfWork.Villas.Update(oldvilla);
				await _unitOfWork.SaveAsync();
				_response.StatusCode = HttpStatusCode.Created;
				_response.IsSuccess = true;
				_response.Result = _mapper.Map<VillaDto>(oldvilla);
				return Ok(_response);
			}
			catch (Exception ex) {

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
				var oldvilla = await _unitOfWork.Villas.GetOrDefaultAsync(v => v.Id == id);
				if (oldvilla == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				patch.ApplyTo(oldvilla);
				_unitOfWork.Villas.Update(oldvilla);
				await _unitOfWork.SaveAsync();
				_response.IsSuccess = true;
				_response.StatusCode=HttpStatusCode.OK;
				_response.Result = _mapper.Map<VillaDto>(oldvilla);
				return Ok(_response);
			}
			catch(Exception ex)
			{
				_response.StatusCode = HttpStatusCode.InternalServerError;
				_response.IsSuccess = false;
				_response.Errors = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, _response);
			}
			
		}
	}
}

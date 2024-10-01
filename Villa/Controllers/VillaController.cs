using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaApp.Models;
using VillaApp.Models.Repository.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.JsonPatch;
using VillaApp.Models.Dtos.VillaDto;
using AutoMapper;

namespace VillaApp.Controllers
{
    [Route("api/Villa")]
	[ApiController]
	public class VillaController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public VillaController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }

        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult GetVillas()
		{
			var villas = _unitOfWork.Villas.GetAll();
			return Ok(villas);	
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult GetVillas(Guid id)
		{
			var villa = _unitOfWork.Villas.GetOrDefault(v => v.Id == id);
			if (villa == null)
				return BadRequest();
			return Ok(villa);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public ActionResult AddVilla([FromBody] VillaDto villa)
		{
			if (villa == null)
			{
				return BadRequest();
			}

			var newId = Guid.NewGuid();

			Villas newVilla = new Villas()
			{
				Id = newId,
				Name = villa.Name,
				Details = villa.Details,
				Rate = villa.Rate,
				Sqft = villa.Sqft,
				Occupancy = villa.Occupancy,
				ImageUrl = villa.ImageUrl,
				Amenity = villa.Amenity,
			};

			_unitOfWork.Villas.Add(newVilla);
			_unitOfWork.Save();
			return CreatedAtAction(nameof(GetVillas), new { id = newId }, newVilla);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult DeleteVilla(Guid id)
		{
			var villa = _unitOfWork.Villas.GetOrDefault(villa => villa.Id == id);
			if(villa == null)
			{
				return BadRequest();
			}
			_unitOfWork.Villas.Delete(villa);
			_unitOfWork.Save();
			return Ok();
		}
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult Update(Guid id,[FromBody]VillaDto villa)
		{
			var oldvilla = _unitOfWork.Villas.GetOrDefault(v => v.Id == id);
			if (oldvilla == null)
			{
				return BadRequest();
			}
			oldvilla.Name = villa.Name;
			oldvilla.Details = villa.Details;
			oldvilla.Rate = villa.Rate;
			oldvilla.Sqft = villa.Sqft;
			oldvilla.Occupancy = villa.Occupancy;
			oldvilla.ImageUrl = villa.ImageUrl;
			oldvilla.Amenity = villa.Amenity;

			_unitOfWork.Villas.Update(oldvilla);
			_unitOfWork.Save();
			return CreatedAtAction(nameof(GetVillas), new { id = oldvilla.Id }, oldvilla);
		}


		[HttpPatch("{id}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public ActionResult UpdatePatch(Guid id, [FromBody] JsonPatchDocument patch)
		{
			var oldvilla = _unitOfWork.Villas.GetOrDefault(v => v.Id == id);
			if (oldvilla == null)
			{
				return BadRequest();
			}

			patch.ApplyTo(oldvilla);
			_unitOfWork.Villas.Update(oldvilla);
			_unitOfWork.Save();
			return CreatedAtAction(nameof(GetVillas), new { id = oldvilla.Id }, oldvilla);
		}
	}
}

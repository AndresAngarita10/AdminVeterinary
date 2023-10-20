
using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize]
public class SpecialityControllers : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public SpecialityControllers(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpecialityDto>>> Get()
    {
        var Speciality = await unitofwork.Specialities.GetAllAsync();
        return mapper.Map<List<SpecialityDto>>(Speciality);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpecialityDto>>> Get([FromQuery] Params Parameters)
    {
        var Speciality = await unitofwork.Specialities.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<SpecialityDto>>(Speciality.registros);
        return Ok(new Pager<SpecialityDto>(listEntidad, Speciality.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecialityDto>> Get(int id)
    {
        var Speciality = await unitofwork.Specialities.GetByIdAsync(id);
        if (Speciality == null)
        {
            return NotFound();
        }
        return this.mapper.Map<SpecialityDto>(Speciality);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Speciality>> Post(SpecialityDto SpecialityDto)
    {
        var Speciality = this.mapper.Map<Speciality>(SpecialityDto);
        this.unitofwork.Specialities.Add(Speciality);
        await unitofwork.SaveAsync();
        if (Speciality == null)
        {
            return BadRequest();
        }
        SpecialityDto.Id = Speciality.Id;
        return CreatedAtAction(nameof(Post), new { id = SpecialityDto.Id }, SpecialityDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecialityDto>> Put(int id, [FromBody] SpecialityDto SpecialityDto)
    {
        if (SpecialityDto == null)
        {
            return NotFound();
        }
        var Speciality = this.mapper.Map<Speciality>(SpecialityDto);
        unitofwork.Specialities.Update(Speciality);
        await unitofwork.SaveAsync();
        return SpecialityDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Speciality = await unitofwork.Specialities.GetByIdAsync(id);
        if (Speciality == null)
        {
            return NotFound();
        }
        unitofwork.Specialities.Remove(Speciality);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}



using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
/* [Authorize] */

public class SpecieController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public SpecieController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpecieDto>>> Get()
    {
        var Specie = await unitofwork.Species.GetAllAsync();
        return mapper.Map<List<SpecieDto>>(Specie);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpecieDto>>> Get([FromQuery] Params Parameters)
    {
        var Specie = await unitofwork.Species.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<SpecieDto>>(Specie.registros);
        return Ok(new Pager<SpecieDto>(listEntidad, Specie.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecieDto>> Get(int id)
    {
        var Specie = await unitofwork.Species.GetByIdAsync(id);
        if (Specie == null)
        {
            return NotFound();
        }
        return this.mapper.Map<SpecieDto>(Specie);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Specie>> Post(SpecieDto SpecieDto)
    {
        var Specie = this.mapper.Map<Specie>(SpecieDto);
        this.unitofwork.Species.Add(Specie);
        await unitofwork.SaveAsync();
        if (Specie == null)
        {
            return BadRequest();
        }
        SpecieDto.Id = Specie.Id;
        return CreatedAtAction(nameof(Post), new { id = SpecieDto.Id }, SpecieDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecieDto>> Put(int id, [FromBody] SpecieDto SpecieDto)
    {
        if (SpecieDto == null)
        {
            return NotFound();
        }
        var Specie = this.mapper.Map<Specie>(SpecieDto);
        unitofwork.Species.Update(Specie);
        await unitofwork.SaveAsync();
        return SpecieDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Specie = await unitofwork.Species.GetByIdAsync(id);
        if (Specie == null)
        {
            return NotFound();
        }
        unitofwork.Species.Remove(Specie);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


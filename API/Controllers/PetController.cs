
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

public class PetController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PetController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get()
    {
        var Pet = await unitofwork.Pets.GetAllAsync();
        return mapper.Map<List<PetDto>>(Pet);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get([FromQuery] Params Parameters)
    {
        var Pet = await unitofwork.Pets.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<PetDto>>(Pet.registros);
        return Ok(new Pager<PetDto>(listEntidad, Pet.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    
    [HttpGet("Consulta3a")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> MascotasFelinos()
    {
        var Pet = await unitofwork.Pets.MascotasFelinos();
        return mapper.Map<List<object>>(Pet);
    }
    [HttpGet("Consulta3a")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> MascotasFelinos([FromQuery] Params Parameters)
    {
        var Pet = await unitofwork.Pets.MascotasFelinos(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<Object>>(Pet.registros);
        return Ok(new Pager<Object>(listEntidad, Pet.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    
    [HttpGet("Consulta6a")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> MascotasVacunadasPrimerTrim2023()
    {
        var Pet = await unitofwork.Pets.MascotasVacunadasPrimerTrim2023();
        return mapper.Map<List<object>>(Pet);
    }
    [HttpGet("Consulta6a")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> MascotasVacunadasPrimerTrim2023([FromQuery] Params Parameters)
    {
        var Pet = await unitofwork.Pets.MascotasVacunadasPrimerTrim2023(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<Object>>(Pet.registros);
        return Ok(new Pager<Object>(listEntidad, Pet.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }


    
    [HttpGet("Consulta1b")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> EspecieYMascota()
    {
        var Pet = await unitofwork.Pets.EspecieYMascota();
        return mapper.Map<List<object>>(Pet);
    }
    [HttpGet("Consulta1b")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> EspecieYMascota([FromQuery] Params Parameters)
    {
        var Pet = await unitofwork.Pets.EspecieYMascota(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<Object>>(Pet.registros);
        return Ok(new Pager<Object>(listEntidad, Pet.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Get(int id)
    {
        var Pet = await unitofwork.Pets.GetByIdAsync(id);
        if (Pet == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PetDto>(Pet);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pet>> Post(PetDto PetDto)
    {
        var Pet = this.mapper.Map<Pet>(PetDto);
        this.unitofwork.Pets.Add(Pet);
        await unitofwork.SaveAsync();
        if (Pet == null)
        {
            return BadRequest();
        }
        PetDto.Id = Pet.Id;
        return CreatedAtAction(nameof(Post), new { id = PetDto.Id }, PetDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Put(int id, [FromBody] PetDto PetDto)
    {
        if (PetDto == null)
        {
            return NotFound();
        }
        var Pet = this.mapper.Map<Pet>(PetDto);
        unitofwork.Pets.Update(Pet);
        await unitofwork.SaveAsync();
        return PetDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Pet = await unitofwork.Pets.GetByIdAsync(id);
        if (Pet == null)
        {
            return NotFound();
        }
        unitofwork.Pets.Remove(Pet);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


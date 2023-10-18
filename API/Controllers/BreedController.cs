
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
/* [Authorize] */
public class BreedController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public BreedController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BreedDto>>> Get()
    {
        var Breed = await unitofwork.Breeds.GetAllAsync();
        return mapper.Map<List<BreedDto>>(Breed);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BreedDto>>> Get([FromQuery] Params Parameters)
    {
        var Breed = await unitofwork.Breeds.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<BreedDto>>(Breed.registros);
        return Ok(new Pager<BreedDto>(listEntidad, Breed.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }
    /* 
    
    public async Task<ActionResult<Pager<RazaDto>>> GetPagination([FromQuery] Params paisParams)
    {
        var entidad = await unitofwork.Razas.GetAllAsync(paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        var listEntidad = mapper.Map<List<RazaDto>>(entidad.registros);
        return new Pager<RazaDto>(listEntidad, entidad.totalRegistros, paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
    }
     */

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Get(int id)
    {
        var Breed = await unitofwork.Breeds.GetByIdAsync(id);
        if (Breed == null)
        {
            return NotFound();
        }
        return this.mapper.Map<BreedDto>(Breed);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Breed>> Post(BreedDto BreedDto)
    {
        var Breed = this.mapper.Map<Breed>(BreedDto);
        this.unitofwork.Breeds.Add(Breed);
        await unitofwork.SaveAsync();
        if (Breed == null)
        {
            return BadRequest();
        }
        BreedDto.Id = Breed.Id;
        return CreatedAtAction(nameof(Post), new { id = BreedDto.Id }, BreedDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Put(int id, [FromBody] BreedDto BreedDto)
    {
        if (BreedDto == null)
        {
            return NotFound();
        }
        var Breed = this.mapper.Map<Breed>(BreedDto);
        unitofwork.Breeds.Update(Breed);
        await unitofwork.SaveAsync();
        return BreedDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Breed = await unitofwork.Breeds.GetByIdAsync(id);
        if (Breed == null)
        {
            return NotFound();
        }
        unitofwork.Breeds.Remove(Breed);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}

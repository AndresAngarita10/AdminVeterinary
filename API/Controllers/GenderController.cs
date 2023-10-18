
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
public class GenderController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public GenderController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GenderDto>>> Get()
    {
        var Gender = await unitofwork.Genders.GetAllAsync();
        return mapper.Map<List<GenderDto>>(Gender);
    }

    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GenderDto>>> Get([FromQuery] Params Parameters)
    {
        var Gender = await unitofwork.Genders.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<GenderDto>>(Gender.registros);
        return Ok(new Pager<GenderDto>(listEntidad, Gender.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenderDto>> Get(int id)
    {
        var Gender = await unitofwork.Genders.GetByIdAsync(id);
        if (Gender == null){
            return NotFound();
        }
        return this.mapper.Map<GenderDto>(Gender);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Gender>> Post(GenderDto GenderDto)
    {
        var Gender = this.mapper.Map<Gender>(GenderDto);
        this.unitofwork.Genders.Add(Gender);
        await unitofwork.SaveAsync();
        if(Gender == null)
        {
            return BadRequest();
        }
        GenderDto.Id = Gender.Id;
        return CreatedAtAction(nameof(Post), new {id = GenderDto.Id}, GenderDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenderDto>> Put(int id, [FromBody]GenderDto GenderDto){
        if(GenderDto == null)
        {
            return NotFound();
        }
        var Gender = this.mapper.Map<Gender>(GenderDto);
        unitofwork.Genders.Update(Gender);
        await unitofwork.SaveAsync();
        return GenderDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var Gender = await unitofwork.Genders.GetByIdAsync(id);
        if(Gender == null)
        {
            return NotFound();
        }
        unitofwork.Genders.Remove(Gender);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


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

public class LaboratoryController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public LaboratoryController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LaboratoryDto>>> Get()
    {
        var Laboratory = await unitofwork.Laboratories.GetAllAsync();
        return mapper.Map<List<LaboratoryDto>>(Laboratory);
    }

    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LaboratoryDto>>> Get([FromQuery] Params Parameters)
    {
        var Laboratory = await unitofwork.Laboratories.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<LaboratoryDto>>(Laboratory.registros);
        return Ok(new Pager<LaboratoryDto>(listEntidad, Laboratory.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LaboratoryDto>> Get(int id)
    {
        var Laboratory = await unitofwork.Laboratories.GetByIdAsync(id);
        if (Laboratory == null){
            return NotFound();
        }
        return this.mapper.Map<LaboratoryDto>(Laboratory);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Laboratory>> Post(LaboratoryDto LaboratoryDto)
    {
        var Laboratory = this.mapper.Map<Laboratory>(LaboratoryDto);
        this.unitofwork.Laboratories.Add(Laboratory);
        await unitofwork.SaveAsync();
        if(Laboratory == null)
        {
            return BadRequest();
        }
        LaboratoryDto.Id = Laboratory.Id;
        return CreatedAtAction(nameof(Post), new {id = LaboratoryDto.Id}, LaboratoryDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LaboratoryDto>> Put(int id, [FromBody]LaboratoryDto LaboratoryDto){
        if(LaboratoryDto == null)
        {
            return NotFound();
        }
        var Laboratory = this.mapper.Map<Laboratory>(LaboratoryDto);
        unitofwork.Laboratories.Update(Laboratory);
        await unitofwork.SaveAsync();
        return LaboratoryDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var Laboratory = await unitofwork.Laboratories.GetByIdAsync(id);
        if(Laboratory == null)
        {
            return NotFound();
        }
        unitofwork.Laboratories.Remove(Laboratory);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


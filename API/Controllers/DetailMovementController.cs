
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

public class DetailMovementController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public DetailMovementController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetailMovementDto>>> Get()
    {
        var DetailMovement = await unitofwork.DetailMovements.GetAllAsync();
        return mapper.Map<List<DetailMovementDto>>(DetailMovement);
    }

    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetailMovementDto>>> Get([FromQuery] Params Parameters)
    {
        var DetailMovement = await unitofwork.DetailMovements.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<DetailMovementDto>>(DetailMovement.registros);
        return Ok(new Pager<DetailMovementDto>(listEntidad, DetailMovement.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailMovementDto>> Get(int id)
    {
        var DetailMovement = await unitofwork.DetailMovements.GetByIdAsync(id);
        if (DetailMovement == null){
            return NotFound();
        }
        return this.mapper.Map<DetailMovementDto>(DetailMovement);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetailMovement>> Post(DetailMovementDto DetailMovementDto)
    {
        var DetailMovement = this.mapper.Map<DetailMovement>(DetailMovementDto);
        this.unitofwork.DetailMovements.Add(DetailMovement);
        await unitofwork.SaveAsync();
        if(DetailMovement == null)
        {
            return BadRequest();
        }
        DetailMovementDto.Id = DetailMovement.Id;
        return CreatedAtAction(nameof(Post), new {id = DetailMovementDto.Id}, DetailMovementDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailMovementDto>> Put(int id, [FromBody]DetailMovementDto DetailMovementDto){
        if(DetailMovementDto == null)
        {
            return NotFound();
        }
        var DetailMovement = this.mapper.Map<DetailMovement>(DetailMovementDto);
        unitofwork.DetailMovements.Update(DetailMovement);
        await unitofwork.SaveAsync();
        return DetailMovementDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var DetailMovement = await unitofwork.DetailMovements.GetByIdAsync(id);
        if(DetailMovement == null)
        {
            return NotFound();
        }
        unitofwork.DetailMovements.Remove(DetailMovement);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}

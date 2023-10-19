
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
public class TypeMovementController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TypeMovementController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TypeMovementDto>>> Get()
    {
        var TypeMovement = await unitofwork.TypeMovements.GetAllAsync();
        return mapper.Map<List<TypeMovementDto>>(TypeMovement);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TypeMovementDto>>> Get([FromQuery] Params Parameters)
    {
        var TypeMovement = await unitofwork.TypeMovements.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<TypeMovementDto>>(TypeMovement.registros);
        return Ok(new Pager<TypeMovementDto>(listEntidad, TypeMovement.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }
   

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TypeMovementDto>> Get(int id)
    {
        var TypeMovement = await unitofwork.TypeMovements.GetByIdAsync(id);
        if (TypeMovement == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TypeMovementDto>(TypeMovement);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TypeMovement>> Post(TypeMovementDto TypeMovementDto)
    {
        var TypeMovement = this.mapper.Map<TypeMovement>(TypeMovementDto);
        this.unitofwork.TypeMovements.Add(TypeMovement);
        await unitofwork.SaveAsync();
        if (TypeMovement == null)
        {
            return BadRequest();
        }
        TypeMovementDto.Id = TypeMovement.Id;
        return CreatedAtAction(nameof(Post), new { id = TypeMovementDto.Id }, TypeMovementDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TypeMovementDto>> Put(int id, [FromBody] TypeMovementDto TypeMovementDto)
    {
        if (TypeMovementDto == null)
        {
            return NotFound();
        }
        var TypeMovement = this.mapper.Map<TypeMovement>(TypeMovementDto);
        unitofwork.TypeMovements.Update(TypeMovement);
        await unitofwork.SaveAsync();
        return TypeMovementDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var TypeMovement = await unitofwork.TypeMovements.GetByIdAsync(id);
        if (TypeMovement == null)
        {
            return NotFound();
        }
        unitofwork.TypeMovements.Remove(TypeMovement);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


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

public class MedicineMovementController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public MedicineMovementController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicineMovementDto>>> Get()
    {
        var MedicineMovement = await unitofwork.MedicineMovements.GetAllAsync();
        return mapper.Map<List<MedicineMovementDto>>(MedicineMovement);
    }
    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicineMovementDto>>> Get([FromQuery] Params Parameters)
    {
        var MedicineMovement = await unitofwork.MedicineMovements.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<MedicineMovementDto>>(MedicineMovement.registros);
        return Ok(new Pager<MedicineMovementDto>(listEntidad, MedicineMovement.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    
    [HttpGet("Consulta2b")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> MovMedicamentoYTotal()
    {
        var Pet = await unitofwork.MedicineMovements.MovMedicamentoYTotal();
        return mapper.Map<List<object>>(Pet);
    }
    /* [HttpGet("Consulta2b")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Object>>> EspecieYMascota([FromQuery] Params Parameters)
    {
        var Pet = await unitofwork.Pets.EspecieYMascota(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<Object>>(Pet.registros);
        return Ok(new Pager<Object>(listEntidad, Pet.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    } */

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicineMovementDto>> Get(int id)
    {
        var MedicineMovement = await unitofwork.MedicineMovements.GetByIdAsync(id);
        if (MedicineMovement == null)
        {
            return NotFound();
        }
        return this.mapper.Map<MedicineMovementDto>(MedicineMovement);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineMovement>> Post(MedicineMovementDto MedicineMovementDto)
    {
        var MedicineMovement = this.mapper.Map<MedicineMovement>(MedicineMovementDto);
        this.unitofwork.MedicineMovements.Add(MedicineMovement);
        await unitofwork.SaveAsync();
        if (MedicineMovement == null)
        {
            return BadRequest();
        }
        MedicineMovementDto.Id = MedicineMovement.Id;
        return CreatedAtAction(nameof(Post), new { id = MedicineMovementDto.Id }, MedicineMovementDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicineMovementDto>> Put(int id, [FromBody] MedicineMovementDto MedicineMovementDto)
    {
        if (MedicineMovementDto == null)
        {
            return NotFound();
        }
        var MedicineMovement = this.mapper.Map<MedicineMovement>(MedicineMovementDto);
        unitofwork.MedicineMovements.Update(MedicineMovement);
        await unitofwork.SaveAsync();
        return MedicineMovementDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var MedicineMovement = await unitofwork.MedicineMovements.GetByIdAsync(id);
        if (MedicineMovement == null)
        {
            return NotFound();
        }
        unitofwork.MedicineMovements.Remove(MedicineMovement);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


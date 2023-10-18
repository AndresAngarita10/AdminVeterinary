
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

public class PartnerController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PartnerController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PartnerDto>>> Get()
    {
        var Partner = await unitofwork.Partners.GetAllAsync();
        return mapper.Map<List<PartnerDto>>(Partner);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PartnerDto>>> Get([FromQuery] Params Parameters)
    {
        var Partner = await unitofwork.Partners.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<PartnerDto>>(Partner.registros);
        return Ok(new Pager<PartnerDto>(listEntidad, Partner.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }
    
    [HttpGet("consulta1")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> VeterinarioCirujanoVascular()
    {
        var Partner = await unitofwork.Partners.VeterinarioCirujanoVascular();
        return mapper.Map<List<object>>(Partner);
    }
    [HttpGet("consulta1")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PartnerDto>>> VeterinarioCirujanoVascular([FromQuery] Params Parameters)
    {
        var Partner = await unitofwork.Partners.VeterinarioCirujanoVascular(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<PartnerDto>>(Partner.registros);
        return Ok(new Pager<PartnerDto>(listEntidad, Partner.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartnerDto>> Get(int id)
    {
        var Partner = await unitofwork.Partners.GetByIdAsync(id);
        if (Partner == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PartnerDto>(Partner);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Partner>> Post(PartnerDto PartnerDto)
    {
        var Partner = this.mapper.Map<Partner>(PartnerDto);
        this.unitofwork.Partners.Add(Partner);
        await unitofwork.SaveAsync();
        if (Partner == null)
        {
            return BadRequest();
        }
        PartnerDto.Id = Partner.Id;
        return CreatedAtAction(nameof(Post), new { id = PartnerDto.Id }, PartnerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartnerDto>> Put(int id, [FromBody] PartnerDto PartnerDto)
    {
        if (PartnerDto == null)
        {
            return NotFound();
        }
        var Partner = this.mapper.Map<Partner>(PartnerDto);
        unitofwork.Partners.Update(Partner);
        await unitofwork.SaveAsync();
        return PartnerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Partner = await unitofwork.Partners.GetByIdAsync(id);
        if (Partner == null)
        {
            return NotFound();
        }
        unitofwork.Partners.Remove(Partner);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


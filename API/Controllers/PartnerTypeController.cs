
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

public class PartnerTypeController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PartnerTypeController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PartnerTypeDto>>> Get()
    {
        var PartnerType = await unitofwork.PartnerTypes.GetAllAsync();
        return mapper.Map<List<PartnerTypeDto>>(PartnerType);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PartnerTypeDto>>> Get([FromQuery] Params Parameters)
    {
        var PartnerType = await unitofwork.PartnerTypes.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<PartnerTypeDto>>(PartnerType.registros);
        return Ok(new Pager<PartnerTypeDto>(listEntidad, PartnerType.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }
   

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartnerTypeDto>> Get(int id)
    {
        var PartnerType = await unitofwork.PartnerTypes.GetByIdAsync(id);
        if (PartnerType == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PartnerTypeDto>(PartnerType);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PartnerType>> Post(PartnerTypeDto PartnerTypeDto)
    {
        var PartnerType = this.mapper.Map<PartnerType>(PartnerTypeDto);
        this.unitofwork.PartnerTypes.Add(PartnerType);
        await unitofwork.SaveAsync();
        if (PartnerType == null)
        {
            return BadRequest();
        }
        PartnerTypeDto.Id = PartnerType.Id;
        return CreatedAtAction(nameof(Post), new { id = PartnerTypeDto.Id }, PartnerTypeDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartnerTypeDto>> Put(int id, [FromBody] PartnerTypeDto PartnerTypeDto)
    {
        if (PartnerTypeDto == null)
        {
            return NotFound();
        }
        var PartnerType = this.mapper.Map<PartnerType>(PartnerTypeDto);
        unitofwork.PartnerTypes.Update(PartnerType);
        await unitofwork.SaveAsync();
        return PartnerTypeDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var PartnerType = await unitofwork.PartnerTypes.GetByIdAsync(id);
        if (PartnerType == null)
        {
            return NotFound();
        }
        unitofwork.PartnerTypes.Remove(PartnerType);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}

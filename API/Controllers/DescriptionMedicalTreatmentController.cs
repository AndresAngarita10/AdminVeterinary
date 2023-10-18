
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

public class DescriptionMedicalTreatmentController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public DescriptionMedicalTreatmentController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DescriptionMedicalTreatmentDto>>> Get()
    {
        var DescriptionMedicalTreatment = await unitofwork.DescriptionMedicalTreatments.GetAllAsync();
        return mapper.Map<List<DescriptionMedicalTreatmentDto>>(DescriptionMedicalTreatment);
    }

    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DescriptionMedicalTreatmentDto>>> Get([FromQuery] Params Parameters)
    {
        var DescriptionMedicalTreatment = await unitofwork.DescriptionMedicalTreatments.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<DescriptionMedicalTreatmentDto>>(DescriptionMedicalTreatment.registros);
        return Ok(new Pager<DescriptionMedicalTreatmentDto>(listEntidad, DescriptionMedicalTreatment.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DescriptionMedicalTreatmentDto>> Get(int id)
    {
        var DescriptionMedicalTreatment = await unitofwork.DescriptionMedicalTreatments.GetByIdAsync(id);
        if (DescriptionMedicalTreatment == null){
            return NotFound();
        }
        return this.mapper.Map<DescriptionMedicalTreatmentDto>(DescriptionMedicalTreatment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DescriptionMedicalTreatment>> Post(DescriptionMedicalTreatmentDto DescriptionMedicalTreatmentDto)
    {
        var DescriptionMedicalTreatment = this.mapper.Map<DescriptionMedicalTreatment>(DescriptionMedicalTreatmentDto);
        this.unitofwork.DescriptionMedicalTreatments.Add(DescriptionMedicalTreatment);
        await unitofwork.SaveAsync();
        if(DescriptionMedicalTreatment == null)
        {
            return BadRequest();
        }
        DescriptionMedicalTreatmentDto.Id = DescriptionMedicalTreatment.Id;
        return CreatedAtAction(nameof(Post), new {id = DescriptionMedicalTreatmentDto.Id}, DescriptionMedicalTreatmentDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DescriptionMedicalTreatmentDto>> Put(int id, [FromBody]DescriptionMedicalTreatmentDto DescriptionMedicalTreatmentDto){
        if(DescriptionMedicalTreatmentDto == null)
        {
            return NotFound();
        }
        var DescriptionMedicalTreatment = this.mapper.Map<DescriptionMedicalTreatment>(DescriptionMedicalTreatmentDto);
        unitofwork.DescriptionMedicalTreatments.Update(DescriptionMedicalTreatment);
        await unitofwork.SaveAsync();
        return DescriptionMedicalTreatmentDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var DescriptionMedicalTreatment = await unitofwork.DescriptionMedicalTreatments.GetByIdAsync(id);
        if(DescriptionMedicalTreatment == null)
        {
            return NotFound();
        }
        unitofwork.DescriptionMedicalTreatments.Remove(DescriptionMedicalTreatment);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}

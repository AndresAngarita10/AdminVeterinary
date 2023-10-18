
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

public class MedicalTreatmentController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MedicalTreatmentController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicalTreatmentDto>>> Get()
    {
        var MedicalTreatment = await unitofwork.MedicalTreatments.GetAllAsync();
        return mapper.Map<List<MedicalTreatmentDto>>(MedicalTreatment);
    }
    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicalTreatmentDto>>> Get([FromQuery] Params Parameters)
    {
        var MedicalTreatment = await unitofwork.MedicalTreatments.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<MedicalTreatmentDto>>(MedicalTreatment.registros);
        return Ok(new Pager<MedicalTreatmentDto>(listEntidad, MedicalTreatment.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicalTreatmentDto>> Get(int id)
    {
        var MedicalTreatment = await unitofwork.MedicalTreatments.GetByIdAsync(id);
        if (MedicalTreatment == null){
            return NotFound();
        }
        return this.mapper.Map<MedicalTreatmentDto>(MedicalTreatment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicalTreatment>> Post(MedicalTreatmentDto MedicalTreatmentDto)
    {
        var MedicalTreatment = this.mapper.Map<MedicalTreatment>(MedicalTreatmentDto);
        this.unitofwork.MedicalTreatments.Add(MedicalTreatment);
        await unitofwork.SaveAsync();
        if(MedicalTreatment == null)
        {
            return BadRequest();
        }
        MedicalTreatmentDto.Id = MedicalTreatment.Id;
        return CreatedAtAction(nameof(Post), new {id = MedicalTreatmentDto.Id}, MedicalTreatmentDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicalTreatmentDto>> Put(int id, [FromBody]MedicalTreatmentDto MedicalTreatmentDto){
        if(MedicalTreatmentDto == null)
        {
            return NotFound();
        }
        var MedicalTreatment = this.mapper.Map<MedicalTreatment>(MedicalTreatmentDto);
        unitofwork.MedicalTreatments.Update(MedicalTreatment);
        await unitofwork.SaveAsync();
        return MedicalTreatmentDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var MedicalTreatment = await unitofwork.MedicalTreatments.GetByIdAsync(id);
        if(MedicalTreatment == null)
        {
            return NotFound();
        }
        unitofwork.MedicalTreatments.Remove(MedicalTreatment);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


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

public class MedicineController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MedicineController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicineDto>>> Get()
    {
        var Medicine = await unitofwork.Medicines.GetAllAsync();
        return mapper.Map<List<MedicineDto>>(Medicine);
    }

    
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicineDto>>> Get([FromQuery] Params Parameters)
    {
        var Medicine = await unitofwork.Medicines.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<MedicineDto>>(Medicine.registros);
        return Ok(new Pager<MedicineDto>(listEntidad, Medicine.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicineDto>> Get(int id)
    {
        var Medicine = await unitofwork.Medicines.GetByIdAsync(id);
        if (Medicine == null){
            return NotFound();
        }
        return this.mapper.Map<MedicineDto>(Medicine);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicine>> Post(MedicineDto MedicineDto)
    {
        var Medicine = this.mapper.Map<Medicine>(MedicineDto);
        this.unitofwork.Medicines.Add(Medicine);
        await unitofwork.SaveAsync();
        if(Medicine == null)
        {
            return BadRequest();
        }
        MedicineDto.Id = Medicine.Id;
        return CreatedAtAction(nameof(Post), new {id = MedicineDto.Id}, MedicineDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicineDto>> Put(int id, [FromBody]MedicineDto MedicineDto){
        if(MedicineDto == null)
        {
            return NotFound();
        }
        var Medicine = this.mapper.Map<Medicine>(MedicineDto);
        unitofwork.Medicines.Update(Medicine);
        await unitofwork.SaveAsync();
        return MedicineDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var Medicine = await unitofwork.Medicines.GetByIdAsync(id);
        if(Medicine == null)
        {
            return NotFound();
        }
        unitofwork.Medicines.Remove(Medicine);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}
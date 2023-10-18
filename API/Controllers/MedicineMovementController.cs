
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MedicineMovementController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MedicineMovementController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicineMovementDto>>> Get()
    {
        var MedicineMovement = await unitofwork.MedicineMovements.GetAllAsync();
        return mapper.Map<List<MedicineMovementDto>>(MedicineMovement);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicineMovementDto>> Get(int id)
    {
        var MedicineMovement = await unitofwork.MedicineMovements.GetByIdAsync(id);
        if (MedicineMovement == null){
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
        if(MedicineMovement == null)
        {
            return BadRequest();
        }
        MedicineMovementDto.Id = MedicineMovement.Id;
        return CreatedAtAction(nameof(Post), new {id = MedicineMovementDto.Id}, MedicineMovementDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicineMovementDto>> Put(int id, [FromBody]MedicineMovementDto MedicineMovementDto){
        if(MedicineMovementDto == null)
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
    public async Task<IActionResult> Delete(int id){
        var MedicineMovement = await unitofwork.MedicineMovements.GetByIdAsync(id);
        if(MedicineMovement == null)
        {
            return NotFound();
        }
        unitofwork.MedicineMovements.Remove(MedicineMovement);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


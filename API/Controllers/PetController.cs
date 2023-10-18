
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PetController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public PetController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get()
    {
        var Pet = await unitofwork.Pets.GetAllAsync();
        return mapper.Map<List<PetDto>>(Pet);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Get(int id)
    {
        var Pet = await unitofwork.Pets.GetByIdAsync(id);
        if (Pet == null){
            return NotFound();
        }
        return this.mapper.Map<PetDto>(Pet);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pet>> Post(PetDto PetDto)
    {
        var Pet = this.mapper.Map<Pet>(PetDto);
        this.unitofwork.Pets.Add(Pet);
        await unitofwork.SaveAsync();
        if(Pet == null)
        {
            return BadRequest();
        }
        PetDto.Id = Pet.Id;
        return CreatedAtAction(nameof(Post), new {id = PetDto.Id}, PetDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Put(int id, [FromBody]PetDto PetDto){
        if(PetDto == null)
        {
            return NotFound();
        }
        var Pet = this.mapper.Map<Pet>(PetDto);
        unitofwork.Pets.Update(Pet);
        await unitofwork.SaveAsync();
        return PetDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var Pet = await unitofwork.Pets.GetByIdAsync(id);
        if(Pet == null)
        {
            return NotFound();
        }
        unitofwork.Pets.Remove(Pet);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


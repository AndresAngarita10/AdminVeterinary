
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SpecialityControllers: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public SpecialityControllers(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpecialityDto>>> Get()
    {
        var Speciality = await unitofwork.Specialities.GetAllAsync();
        return mapper.Map<List<SpecialityDto>>(Speciality);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecialityDto>> Get(int id)
    {
        var Speciality = await unitofwork.Specialities.GetByIdAsync(id);
        if (Speciality == null){
            return NotFound();
        }
        return this.mapper.Map<SpecialityDto>(Speciality);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Speciality>> Post(SpecialityDto SpecialityDto)
    {
        var Speciality = this.mapper.Map<Speciality>(SpecialityDto);
        this.unitofwork.Specialities.Add(Speciality);
        await unitofwork.SaveAsync();
        if(Speciality == null)
        {
            return BadRequest();
        }
        SpecialityDto.Id = Speciality.Id;
        return CreatedAtAction(nameof(Post), new {id = SpecialityDto.Id}, SpecialityDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecialityDto>> Put(int id, [FromBody]SpecialityDto SpecialityDto){
        if(SpecialityDto == null)
        {
            return NotFound();
        }
        var Speciality = this.mapper.Map<Speciality>(SpecialityDto);
        unitofwork.Specialities.Update(Speciality);
        await unitofwork.SaveAsync();
        return SpecialityDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var Speciality = await unitofwork.Specialities.GetByIdAsync(id);
        if(Speciality == null)
        {
            return NotFound();
        }
        unitofwork.Specialities.Remove(Speciality);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


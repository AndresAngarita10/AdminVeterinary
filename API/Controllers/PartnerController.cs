
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PartnerController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public PartnerController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PartnerDto>>> Get()
    {
        var Partner = await unitofwork.Partners.GetAllAsync();
        return mapper.Map<List<PartnerDto>>(Partner);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartnerDto>> Get(int id)
    {
        var Partner = await unitofwork.Partners.GetByIdAsync(id);
        if (Partner == null){
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
        if(Partner == null)
        {
            return BadRequest();
        }
        PartnerDto.Id = Partner.Id;
        return CreatedAtAction(nameof(Post), new {id = PartnerDto.Id}, PartnerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartnerDto>> Put(int id, [FromBody]PartnerDto PartnerDto){
        if(PartnerDto == null)
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
    public async Task<IActionResult> Delete(int id){
        var Partner = await unitofwork.Partners.GetByIdAsync(id);
        if(Partner == null)
        {
            return NotFound();
        }
        unitofwork.Partners.Remove(Partner);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


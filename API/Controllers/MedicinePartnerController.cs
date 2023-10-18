
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MedicinePartnerController: ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MedicinePartnerController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicinePartnerDto>>> Get()
    {
        var MedicinePartner = await unitofwork.MedicinePartners.GetAllAsync();
        return mapper.Map<List<MedicinePartnerDto>>(MedicinePartner);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicinePartnerDto>> Get(int id)
    {
        var MedicinePartner = await unitofwork.MedicinePartners.GetByIdAsync(id);
        if (MedicinePartner == null){
            return NotFound();
        }
        return this.mapper.Map<MedicinePartnerDto>(MedicinePartner);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicinePartner>> Post(MedicinePartnerDto MedicinePartnerDto)
    {
        var MedicinePartner = this.mapper.Map<MedicinePartner>(MedicinePartnerDto);
        this.unitofwork.MedicinePartners.Add(MedicinePartner);
        await unitofwork.SaveAsync();
        if(MedicinePartner == null)
        {
            return BadRequest();
        }
        MedicinePartnerDto.Id = MedicinePartner.Id;
        return CreatedAtAction(nameof(Post), new {id = MedicinePartnerDto.Id}, MedicinePartnerDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicinePartnerDto>> Put(int id, [FromBody]MedicinePartnerDto MedicinePartnerDto){
        if(MedicinePartnerDto == null)
        {
            return NotFound();
        }
        var MedicinePartner = this.mapper.Map<MedicinePartner>(MedicinePartnerDto);
        unitofwork.MedicinePartners.Update(MedicinePartner);
        await unitofwork.SaveAsync();
        return MedicinePartnerDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var MedicinePartner = await unitofwork.MedicinePartners.GetByIdAsync(id);
        if(MedicinePartner == null)
        {
            return NotFound();
        }
        unitofwork.MedicinePartners.Remove(MedicinePartner);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}


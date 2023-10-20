
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

public class QuoteController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public QuoteController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<QuoteDto>>> Get()
    {
        var Quote = await unitofwork.Quotes.GetAllAsync();
        return mapper.Map<List<QuoteDto>>(Quote);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<QuoteDto>>> Get([FromQuery] Params Parameters)
    {
        var Quote = await unitofwork.Quotes.GetAllAsync(Parameters.PageIndex, Parameters.PageSize, Parameters.Search);
        var listEntidad = mapper.Map<List<QuoteDto>>(Quote.registros);
        return Ok(new Pager<QuoteDto>(listEntidad, Quote.totalRegistros, Parameters.PageIndex, Parameters.PageSize, Parameters.Search));

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuoteDto>> Get(int id)
    {
        var Quote = await unitofwork.Quotes.GetByIdAsync(id);
        if (Quote == null)
        {
            return NotFound();
        }
        return this.mapper.Map<QuoteDto>(Quote);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Quote>> Post(QuoteDto QuoteDto)
    {
        var Quote = this.mapper.Map<Quote>(QuoteDto);
        this.unitofwork.Quotes.Add(Quote);
        await unitofwork.SaveAsync();
        if (Quote == null)
        {
            return BadRequest();
        }
        QuoteDto.Id = Quote.Id;
        return CreatedAtAction(nameof(Post), new { id = QuoteDto.Id }, QuoteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<QuoteDto>> Put(int id, [FromBody] QuoteDto QuoteDto)
    {
        if (QuoteDto == null)
        {
            return NotFound();
        }
        var Quote = this.mapper.Map<Quote>(QuoteDto);
        unitofwork.Quotes.Update(Quote);
        await unitofwork.SaveAsync();
        return QuoteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Quote = await unitofwork.Quotes.GetByIdAsync(id);
        if (Quote == null)
        {
            return NotFound();
        }
        unitofwork.Quotes.Remove(Quote);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}

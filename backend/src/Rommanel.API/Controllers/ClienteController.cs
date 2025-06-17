using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rommanel.Application.Clientes.Commands;
using Rommanel.Application.Clientes.DTOs;
using Rommanel.Application.Clientes.Queries;

namespace Rommanel.API.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClienteController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClienteCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { id }, new { id });
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDto>> GetById(Guid id)
    {
        var cliente = await _mediator.Send(new GetClienteByIdQuery(id));
        return Ok(cliente);
    }
    [HttpGet("search")]
    public async Task<ActionResult<List<ClienteDto>>> Search([FromQuery] string? nome, [FromQuery] string? cidade)
    {
        var query = new SearchClientesQuery(nome, cidade);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteClienteCommand(id));
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteCommand command)
    {
        await _mediator.Send(new UpdateClienteCommandWrapper(id, command));
        return NoContent();
    }
}
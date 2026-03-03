using Microsoft.AspNetCore.Mvc;
using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Services;

namespace ORBE_WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArmazensController : ControllerBase
{
    private readonly ArmazemAppService _service;

    public ArmazensController(ArmazemAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ArmazemDto>>> ObterTodos()
    {
        var lista = await _service.ObterTodosAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArmazemDto>> ObterPorId(int id)
    {
        var armazem = await _service.ObterPorIdAsync(id);
        if (armazem is null) return NotFound();
        return Ok(armazem);
    }

    [HttpPost]
    public async Task<ActionResult<ArmazemDto>> Criar([FromBody] CriarArmazemDto dto)
    {
        var criado = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarArmazemDto dto)
    {
        if (id != dto.Id) return BadRequest();
        await _service.AtualizarAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _service.RemoverAsync(id);
        return NoContent();
    }
}

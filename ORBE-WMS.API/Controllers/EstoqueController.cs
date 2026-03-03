using Microsoft.AspNetCore.Mvc;
using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Services;

namespace ORBE_WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstoqueController : ControllerBase
{
    private readonly ItemEstoqueAppService _service;

    public EstoqueController(ItemEstoqueAppService service)
    {
        _service = service;
    }

    [HttpGet("armazem/{armazemId}")]
    public async Task<ActionResult<List<ItemEstoqueDto>>> ObterPorArmazem(int armazemId)
    {
        var lista = await _service.ObterPorArmazemAsync(armazemId);
        return Ok(lista);
    }

    [HttpGet("armazem/{armazemId}/depositante/{depositanteId}")]
    public async Task<ActionResult<List<ItemEstoqueDto>>> ObterPorArmazemEDepositante(int armazemId, int depositanteId)
    {
        var lista = await _service.ObterPorArmazemEDepositanteAsync(armazemId, depositanteId);
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemEstoqueDto>> ObterPorId(int id)
    {
        var item = await _service.ObterPorIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult> Criar([FromBody] CriarItemEstoqueDto dto)
    {
        var criado = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarItemEstoqueDto dto)
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

using Microsoft.AspNetCore.Mvc;
using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Services;

namespace ORBE_WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepositantesController : ControllerBase
{
    private readonly DepositanteAppService _service;

    public DepositantesController(DepositanteAppService service)
    {
        _service = service;
    }

    [HttpGet("armazem/{armazemId}")]
    public async Task<ActionResult<List<DepositanteDto>>> ObterPorArmazem(int armazemId)
    {
        var lista = await _service.ObterPorArmazemAsync(armazemId);
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepositanteDto>> ObterPorId(int id)
    {
        var dep = await _service.ObterPorIdAsync(id);
        if (dep is null) return NotFound();
        return Ok(dep);
    }

    [HttpPost]
    public async Task<ActionResult> Criar([FromBody] CriarDepositanteDto dto)
    {
        var criado = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarDepositanteDto dto)
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

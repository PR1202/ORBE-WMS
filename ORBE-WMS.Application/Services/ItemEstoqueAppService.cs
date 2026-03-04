using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Services;

public class ItemEstoqueAppService
{
    private readonly IItemEstoqueRepository _repo;

    public ItemEstoqueAppService(IItemEstoqueRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ItemEstoqueDto>> ObterPorArmazemEDepositanteAsync(int armazemId, int depositanteId)
    {
        var lista = await _repo.ObterPorArmazemEDepositanteAsync(armazemId, depositanteId);
        return lista.Select(MapToDto).ToList();
    }

    public async Task<List<ItemEstoqueDto>> ObterPorArmazemAsync(int armazemId)
    {
        var lista = await _repo.ObterPorArmazemAsync(armazemId);
        return lista.Select(MapToDto).ToList();
    }

    public async Task<ItemEstoqueDto?> ObterPorIdAsync(int id)
    {
        var item = await _repo.ObterPorIdAsync(id);
        if (item is null) return null;
        return MapToDto(item);
    }

    public async Task<ItemEstoque> CriarAsync(CriarItemEstoqueDto dto)
    {
        var item = new ItemEstoque
        {
            ArmazemId = dto.ArmazemId,
            DepositanteId = dto.DepositanteId,
            CodigoProduto = dto.CodigoProduto,
            Descricao = dto.Descricao,
            Quantidade = dto.Quantidade,
            UnidadeMedida = dto.UnidadeMedida,
            Lote = dto.Lote,
            DataValidade = dto.DataValidade,
            Localizacao = dto.Localizacao,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };
        return await _repo.CriarAsync(item);
    }

    public async Task AtualizarAsync(AtualizarItemEstoqueDto dto)
    {
        var item = await _repo.ObterPorIdAsync(dto.Id)
            ?? throw new InvalidOperationException($"Item de estoque {dto.Id} não encontrado.");
        item.CodigoProduto = dto.CodigoProduto;
        item.Descricao = dto.Descricao;
        item.Quantidade = dto.Quantidade;
        item.UnidadeMedida = dto.UnidadeMedida;
        item.Lote = dto.Lote;
        item.DataValidade = dto.DataValidade;
        item.Localizacao = dto.Localizacao;
        item.Ativo = dto.Ativo;
        await _repo.AtualizarAsync(item);
    }

    public async Task RemoverAsync(int id)
    {
        await _repo.RemoverAsync(id);
    }

    public async Task<bool> ToggleAtivoAsync(int id)
    {
        var item = await _repo.ObterPorIdAsync(id)
            ?? throw new InvalidOperationException($"Item de estoque {id} não encontrado.");
        item.Ativo = !item.Ativo;
        await _repo.AtualizarAsync(item);
        return item.Ativo;
    }

    private static ItemEstoqueDto MapToDto(ItemEstoque i) => new()
    {
        Id = i.Id,
        ArmazemId = i.ArmazemId,
        DepositanteId = i.DepositanteId,
        CodigoProduto = i.CodigoProduto,
        Descricao = i.Descricao,
        Quantidade = i.Quantidade,
        UnidadeMedida = i.UnidadeMedida,
        Lote = i.Lote,
        DataValidade = i.DataValidade,
        Localizacao = i.Localizacao,
        Ativo = i.Ativo,
        DataCriacao = i.DataCriacao,
        DepositanteNome = i.Depositante?.Nome ?? string.Empty
    };
}

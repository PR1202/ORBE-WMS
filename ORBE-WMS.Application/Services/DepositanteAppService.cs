using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Services;

public class DepositanteAppService
{
    private readonly IDepositanteRepository _repo;

    public DepositanteAppService(IDepositanteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<DepositanteDto>> ObterPorArmazemAsync(int armazemId)
    {
        var lista = await _repo.ObterPorArmazemAsync(armazemId);
        return lista.Select(d => new DepositanteDto
        {
            Id = d.Id,
            ArmazemId = d.ArmazemId,
            Nome = d.Nome,
            CNPJ = d.CNPJ,
            CodigoExterno = d.CodigoExterno,
            Endereco = d.Endereco,
            Ativo = d.Ativo,
            DataCriacao = d.DataCriacao,
            ArmazemNome = d.Armazem?.Nome ?? string.Empty
        }).ToList();
    }

    public async Task<DepositanteDto?> ObterPorIdAsync(int id)
    {
        var d = await _repo.ObterPorIdAsync(id);
        if (d is null) return null;
        return new DepositanteDto
        {
            Id = d.Id,
            ArmazemId = d.ArmazemId,
            Nome = d.Nome,
            CNPJ = d.CNPJ,
            CodigoExterno = d.CodigoExterno,
            Endereco = d.Endereco,
            Ativo = d.Ativo,
            DataCriacao = d.DataCriacao,
            ArmazemNome = d.Armazem?.Nome ?? string.Empty
        };
    }

    public async Task<Depositante> CriarAsync(CriarDepositanteDto dto)
    {
        var depositante = new Depositante
        {
            ArmazemId = dto.ArmazemId,
            Nome = dto.Nome,
            CNPJ = dto.CNPJ,
            CodigoExterno = dto.CodigoExterno,
            Endereco = dto.Endereco,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };
        return await _repo.CriarAsync(depositante);
    }

    public async Task AtualizarAsync(AtualizarDepositanteDto dto)
    {
        var dep = await _repo.ObterPorIdAsync(dto.Id)
            ?? throw new InvalidOperationException($"Depositante {dto.Id} não encontrado.");
        dep.Nome = dto.Nome;
        dep.CNPJ = dto.CNPJ;
        dep.CodigoExterno = dto.CodigoExterno;
        dep.Endereco = dto.Endereco;
        dep.Ativo = dto.Ativo;
        await _repo.AtualizarAsync(dep);
    }

    public async Task RemoverAsync(int id)
    {
        await _repo.RemoverAsync(id);
    }
}

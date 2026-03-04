using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Services;

public class ArmazemAppService
{
    private readonly IArmazemRepository _repo;

    public ArmazemAppService(IArmazemRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ArmazemDto>> ObterTodosAsync()
    {
        var lista = await _repo.ObterTodosAsync();
        return lista.Select(a => new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao,
            TotalDepositantes = a.Depositantes.Count,
            TotalUsuarios = a.Usuarios.Count
        }).ToList();
    }

    public async Task<List<ArmazemDto>> ObterPorUsuarioAsync(string usuarioId)
    {
        var lista = await _repo.ObterPorUsuarioAsync(usuarioId);
        return lista.Select(a => new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao,
            TotalDepositantes = a.Depositantes.Count,
            TotalUsuarios = a.Usuarios.Count
        }).ToList();
    }

    public async Task<ArmazemDto?> ObterPorIdAsync(int id)
    {
        var a = await _repo.ObterPorIdAsync(id);
        if (a is null) return null;
        return new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao,
            TotalDepositantes = a.Depositantes.Count,
            TotalUsuarios = a.Usuarios.Count
        };
    }

    public async Task<Armazem> CriarAsync(CriarArmazemDto dto)
    {
        var armazem = new Armazem
        {
            Nome = dto.Nome,
            CNPJ = dto.CNPJ,
            Endereco = dto.Endereco,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };
        return await _repo.CriarAsync(armazem);
    }

    public async Task AtualizarAsync(AtualizarArmazemDto dto)
    {
        var armazem = await _repo.ObterPorIdAsync(dto.Id)
            ?? throw new InvalidOperationException($"Armazém {dto.Id} não encontrado.");
        armazem.Nome = dto.Nome;
        armazem.CNPJ = dto.CNPJ;
        armazem.Endereco = dto.Endereco;
        armazem.Ativo = dto.Ativo;
        await _repo.AtualizarAsync(armazem);
    }

    public async Task RemoverAsync(int id)
    {
        await _repo.RemoverAsync(id);
    }

    public async Task<bool> ToggleAtivoAsync(int id)
    {
        var armazem = await _repo.ObterPorIdAsync(id)
            ?? throw new InvalidOperationException($"Armazém {id} não encontrado.");
        armazem.Ativo = !armazem.Ativo;
        await _repo.AtualizarAsync(armazem);
        return armazem.Ativo;
    }

    public async Task<List<ArmazemDto>> ObterAtivosAsync()
    {
        var lista = await _repo.ObterAtivosAsync();
        return lista.Select(a => new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao,
            TotalDepositantes = a.Depositantes.Count,
            TotalUsuarios = a.Usuarios.Count
        }).ToList();
    }

    public async Task<List<ArmazemDto>> ObterAtivosPorUsuarioAsync(string usuarioId)
    {
        var lista = await _repo.ObterAtivosPorUsuarioAsync(usuarioId);
        return lista.Select(a => new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao,
            TotalDepositantes = a.Depositantes.Count,
            TotalUsuarios = a.Usuarios.Count
        }).ToList();
    }
}

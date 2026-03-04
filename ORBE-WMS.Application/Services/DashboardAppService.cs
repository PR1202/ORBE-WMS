using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Interfaces;

namespace ORBE_WMS.Application.Services;

public class DashboardAppService
{
    private readonly IArmazemRepository _armazemRepo;
    private readonly IDepositanteRepository _depositanteRepo;
    private readonly IUsuarioArmazemRepository _usuarioArmazemRepo;
    private readonly IUsuarioRepository _usuarioRepo;

    public DashboardAppService(
        IArmazemRepository armazemRepo,
        IDepositanteRepository depositanteRepo,
        IUsuarioArmazemRepository usuarioArmazemRepo,
        IUsuarioRepository usuarioRepo)
    {
        _armazemRepo = armazemRepo;
        _depositanteRepo = depositanteRepo;
        _usuarioArmazemRepo = usuarioArmazemRepo;
        _usuarioRepo = usuarioRepo;
    }

    public async Task<DashboardDto> ObterDashboardAdminAsync(string userId)
    {
        var nomeUsuario = await _usuarioRepo.ObterNomeAsync(userId) ?? "Usuário";

        var totalArmazens = await _armazemRepo.CountAtivosAsync();
        var totalDepositantes = await _depositanteRepo.CountAtivosAsync();
        var depositantesInativos = await _depositanteRepo.CountInativosAsync();
        var totalUsuarios = await _usuarioRepo.CountAsync();

        var armazensRecentes = await _armazemRepo.ObterRecentesAsync(5);
        var armazensDtos = armazensRecentes.Select(a => new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao
        }).ToList();

        return new DashboardDto
        {
            NomeUsuario = nomeUsuario,
            IsAdmin = true,
            TotalArmazens = totalArmazens,
            TotalDepositantes = totalDepositantes,
            DepositantesInativos = depositantesInativos,
            TotalUsuarios = totalUsuarios,
            ArmazensRecentes = armazensDtos
        };
    }

    public async Task<DashboardDto> ObterDashboardUsuarioAsync(string userId)
    {
        var nomeUsuario = await _usuarioRepo.ObterNomeAsync(userId) ?? "Usuário";

        var meusArmazemIds = await _usuarioArmazemRepo.ObterArmazemIdsPorUsuarioAsync(userId);
        var totalDepositantes = await _depositanteRepo.CountAtivosParaArmazensAsync(meusArmazemIds);

        var armazens = await _armazemRepo.ObterAtivosPorUsuarioAsync(userId);
        var armazensDtos = armazens.Select(a => new ArmazemDto
        {
            Id = a.Id,
            Nome = a.Nome,
            CNPJ = a.CNPJ,
            Endereco = a.Endereco,
            Ativo = a.Ativo,
            DataCriacao = a.DataCriacao
        }).ToList();

        var papeis = await _usuarioArmazemRepo.ObterRolesPorUsuarioAsync(userId);
        var meuPapel = papeis.Count switch
        {
            0 => "Sem acesso",
            1 => papeis[0],
            _ => string.Join(" / ", papeis)
        };

        return new DashboardDto
        {
            NomeUsuario = nomeUsuario,
            IsAdmin = false,
            TotalArmazens = meusArmazemIds.Count,
            TotalDepositantes = totalDepositantes,
            MeuPapel = meuPapel,
            ArmazensRecentes = armazensDtos
        };
    }
}

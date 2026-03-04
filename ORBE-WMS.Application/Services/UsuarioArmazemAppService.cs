using ORBE_WMS.Application.DTOs;
using ORBE_WMS.Application.Interfaces;
using ORBE_WMS.Domain.Entities;

namespace ORBE_WMS.Application.Services;

public class UsuarioArmazemAppService
{
    private readonly IUsuarioArmazemRepository _uaRepo;
    private readonly IArmazemRepository _armazemRepo;
    private readonly IUsuarioRepository _usuarioRepo;

    public UsuarioArmazemAppService(
        IUsuarioArmazemRepository uaRepo,
        IArmazemRepository armazemRepo,
        IUsuarioRepository usuarioRepo)
    {
        _uaRepo = uaRepo;
        _armazemRepo = armazemRepo;
        _usuarioRepo = usuarioRepo;
    }

    /// <summary>
    /// Retorna todos os armazéns ativos com o estado de vínculo do usuário, para o modal de gerenciamento.
    /// </summary>
    public async Task<List<VinculoArmazemDto>> ObterVinculosParaGerenciamentoAsync(string userId)
    {
        var armazensAtivos = await _armazemRepo.ObterAtivosAsync();
        var vinculosExistentes = await _uaRepo.ObterPorUsuarioAsync(userId);

        return armazensAtivos.Select(a =>
        {
            var existente = vinculosExistentes.FirstOrDefault(v => v.ArmazemId == a.Id);
            return new VinculoArmazemDto
            {
                ArmazemId = a.Id,
                ArmazemNome = a.Nome,
                Vinculado = existente is not null,
                Role = existente?.Role ?? "Operador"
            };
        }).ToList();
    }

    /// <summary>
    /// Sincroniza os vínculos de armazéns do usuário (remove todos e recria).
    /// </summary>
    public async Task SincronizarVinculosAsync(string userId, List<VinculoArmazemDto> vinculos)
    {
        var novosVinculos = vinculos
            .Where(v => v.Vinculado)
            .Select(v => new UsuarioArmazem
            {
                UsuarioId = userId,
                ArmazemId = v.ArmazemId,
                Role = v.Role
            })
            .ToList();

        await _uaRepo.SincronizarVinculosAsync(userId, novosVinculos);
    }

    /// <summary>
    /// Retorna todos os usuários com seus vínculos de armazéns (para listagem Usuarios.razor).
    /// </summary>
    public async Task<List<ApplicationUser>> ObterTodosUsuariosComArmazensAsync()
    {
        return await _usuarioRepo.ObterTodosComArmazensAsync();
    }
}

public class VinculoArmazemDto
{
    public int ArmazemId { get; set; }
    public string ArmazemNome { get; set; } = string.Empty;
    public bool Vinculado { get; set; }
    public string Role { get; set; } = "Operador";
}

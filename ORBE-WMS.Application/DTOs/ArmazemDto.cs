namespace ORBE_WMS.Application.DTOs;

public class ArmazemDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CNPJ { get; set; }
    public string? Endereco { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
    public int TotalDepositantes { get; set; }
    public int TotalUsuarios { get; set; }
}

public class CriarArmazemDto
{
    public string Nome { get; set; } = string.Empty;
    public string? CNPJ { get; set; }
    public string? Endereco { get; set; }
}

public class AtualizarArmazemDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CNPJ { get; set; }
    public string? Endereco { get; set; }
    public bool Ativo { get; set; }
}

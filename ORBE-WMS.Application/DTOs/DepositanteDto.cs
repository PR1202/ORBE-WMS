namespace ORBE_WMS.Application.DTOs;

public class DepositanteDto
{
    public int Id { get; set; }
    public int ArmazemId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CNPJ { get; set; }
    public string? CodigoExterno { get; set; }
    public string? Endereco { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
    public string ArmazemNome { get; set; } = string.Empty;
}

public class CriarDepositanteDto
{
    public int ArmazemId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CNPJ { get; set; }
    public string? CodigoExterno { get; set; }
    public string? Endereco { get; set; }
}

public class AtualizarDepositanteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CNPJ { get; set; }
    public string? CodigoExterno { get; set; }
    public string? Endereco { get; set; }
    public bool Ativo { get; set; }
}

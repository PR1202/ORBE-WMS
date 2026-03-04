namespace ORBE_WMS.Application.DTOs;

public class DashboardDto
{
    public string NomeUsuario { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public int TotalArmazens { get; set; }
    public int TotalDepositantes { get; set; }
    public int TotalUsuarios { get; set; }
    public int DepositantesInativos { get; set; }
    public string MeuPapel { get; set; } = "-";
    public List<ArmazemDto> ArmazensRecentes { get; set; } = [];
}

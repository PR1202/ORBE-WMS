namespace ORBE_WMS.WebApp.Services;

public class TabItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = "bi-file-earmark";
    public Type ComponentType { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool IsPinned { get; set; }
}

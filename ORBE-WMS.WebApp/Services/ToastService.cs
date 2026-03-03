namespace ORBE_WMS.WebApp.Services;

/// <summary>
/// Serviço scoped para exibir notificações toast na UI.
/// </summary>
public class ToastService
{
    public event Action<ToastMessage>? OnToast;

    public void Success(string message) =>
        OnToast?.Invoke(new ToastMessage(ToastType.Success, message));

    public void Error(string message) =>
        OnToast?.Invoke(new ToastMessage(ToastType.Error, message));

    public void Warning(string message) =>
        OnToast?.Invoke(new ToastMessage(ToastType.Warning, message));

    public void Info(string message) =>
        OnToast?.Invoke(new ToastMessage(ToastType.Info, message));
}

public enum ToastType
{
    Success,
    Error,
    Warning,
    Info
}

public record ToastMessage(ToastType Type, string Message)
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}

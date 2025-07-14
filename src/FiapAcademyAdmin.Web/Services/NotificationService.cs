using FiapAcademyAdmin.Application.Interfaces.Services;

namespace FiapAcademyAdmin.Web.Services;

public class NotificationService : INotificationService
{
    public event Action<string, string, string>? OnShowToast;

    public void ShowToast(string message, string type, string icon)
    {
        OnShowToast?.Invoke(message, type, icon);
    }
} 
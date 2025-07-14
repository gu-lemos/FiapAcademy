namespace FiapAcademyAdmin.Application.Interfaces.Services;

public interface INotificationService
{
    void ShowToast(string message, string type, string icon);
} 
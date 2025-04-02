using Playtech.Lib;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Playtech.Notification;

public class NotificationViewModel: INotifyPropertyChanged
{
    private bool _isNotificationVisible = false;

    public bool IsNotificationVisible
    {
        get { return _isNotificationVisible; }
        set
        {
            if (_isNotificationVisible != value)
            {
                _isNotificationVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public NotificationViewModel() => 
        EventAggregator.Instance.Subscribe<ShowNotificationMessage>(async _ => await ExecuteShowNotification(null));

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private async Task ExecuteShowNotification(object? _)
    {
        IsNotificationVisible = true;
        OnPropertyChanged(nameof(IsNotificationVisible));

        await Task.Delay(5000);

        IsNotificationVisible = false;
        OnPropertyChanged(nameof(IsNotificationVisible));
    }
}
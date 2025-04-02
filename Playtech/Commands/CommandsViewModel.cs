using Playtech.Lib;
using System.ComponentModel;
using System.Windows.Input;

namespace Playtech.Commands;

public class CommandsViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand AddResultCommand { get; }
    public ICommand ShowNotificationCommand { get; }

    public CommandsViewModel()
    {
        AddResultCommand = new RelayCommand(_ => EventAggregator.Instance.Publish(new AddRandomResultMessage()));
        ShowNotificationCommand = new RelayCommand(_ => EventAggregator.Instance.Publish(new ShowNotificationMessage()));
    }
}

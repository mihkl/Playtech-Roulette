using System.ComponentModel;

namespace Playtech.Statistics;

public class StatisticsViewModel : INotifyPropertyChanged
{
    private StatisticsModel StatisticsModel { get; }
    public Statistics? Statistics => StatisticsModel.Statistics;

    public event PropertyChangedEventHandler? PropertyChanged;

    public StatisticsViewModel(StatisticsModel statisticsModel)
    {
        StatisticsModel = statisticsModel;
        StatisticsModel.PropertyChanged += (sender, args) =>
        {
            OnPropertyChanged(args.PropertyName ?? "");
        };
    }
    protected virtual void OnPropertyChanged(string propertyName = "") => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

using Playtech.Lib;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Playtech.Results;

public class ResultsViewModel : INotifyPropertyChanged
{
    private ResultsModel Model { get; set; }
    public ObservableCollection<ResultsModel.RouletteResult> Results { get; }
    public event PropertyChangedEventHandler? PropertyChanged;

    public ResultsViewModel(ResultsModel model)
    {
        Model = model;
        Results = [];
        EventAggregator.Instance.Subscribe<AddRandomResultMessage>(_ => ExecuteAddRandomResult(null));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void ExecuteAddRandomResult(object? _)
    {
        Model.AddRandomResult();
        Results.Add(Model.Results.Last());
        if (Results.Count > 10)
        {
            Results.RemoveAt(0);
        }
        OnPropertyChanged(nameof(Results));
    }
}

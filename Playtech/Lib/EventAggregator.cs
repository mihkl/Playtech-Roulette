namespace Playtech.Lib;

public class EventAggregator
{
    private static readonly EventAggregator _instance = new();
    public static EventAggregator Instance => _instance;

    private readonly Dictionary<Type, List<Action<object>>> _handlers = [];

    public void Subscribe<TMessage>(Action<TMessage> handler)
    {
        var type = typeof(TMessage);
        if (!_handlers.ContainsKey(type))
            _handlers[type] = [];

        _handlers[type].Add(obj => handler((TMessage)obj));
    }

    public void Publish<TMessage>(TMessage message)
    {
        var type = typeof(TMessage);
        if (_handlers.TryGetValue(type, out List<Action<object>>? value))
        {
            foreach (var handler in value.ToList())
            {
                if (message is not null) handler(message);
            }
        }
    }
}

public class AddRandomResultMessage;
public class ShowNotificationMessage;

using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Playtech.Statistics;

public class StatisticsModel: INotifyPropertyChanged
{
    public Statistics? Statistics { get; private set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public StatisticsModel(int port)
    {
        _port = port;
        _ = StartListeningAsync();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private readonly int _port;
    private TcpListener? listener = null;

    public async Task StartListeningAsync()
    {
        try
        {
            IPAddress localAddr = IPAddress.Any;

            listener = new TcpListener(localAddr, _port);
            listener.Start();

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listening: {ex.Message}");
        }
        finally
        {
            listener?.Stop();
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                ProcessMessage(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling client: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    private void ProcessMessage(string message)
    {
        if (TryParse(message, out var statistics))
        {
            Statistics = new Statistics
            {
                ActivePlayers = statistics!.ActivePlayers,
                BiggestMultiplier = statistics.BiggestMultiplier
            };
            OnPropertyChanged(nameof(Statistics));
        }
    }

    private static bool TryParse(string message, out Statistics? statistics)
    {
        try
        {
            var parsedJson = JsonConvert.DeserializeObject<Statistics>(message);
            if (parsedJson is null)
            {
                statistics = null;
                return false;
            }
            statistics = parsedJson;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            statistics = null;
            return false;
        }
    }
}

public record Statistics
{
    [JsonProperty("activePlayers")]
    public int ActivePlayers { get; init; }
    [JsonProperty("biggestMultiplier")]
    public int BiggestMultiplier { get; init; }
}
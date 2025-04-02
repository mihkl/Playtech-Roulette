using Playtech.Statistics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Playtech.Test;

[TestFixture]
public class StatisticsModelTests
{
    private StatisticsModel _statisticsModel;
    private int _port;

    [SetUp]
    public void Setup()
    {
        _port = GetAvailablePort();
        _statisticsModel = new StatisticsModel(_port);
    }

    [TearDown]
    public void Cleanup() { }
    

    [Test]
    public async Task ProcessMessage_ValidJson_UpdatesStatistics()
    {
        var statistics = new Playtech.Statistics.Statistics { ActivePlayers = 100, BiggestMultiplier = 1000 };
        string json = JsonConvert.SerializeObject(statistics);

        await SendMessage(json);
        await Task.Delay(100);

        Assert.That(_statisticsModel.Statistics, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(_statisticsModel.Statistics.ActivePlayers, Is.EqualTo(statistics.ActivePlayers));
            Assert.That(_statisticsModel.Statistics.BiggestMultiplier, Is.EqualTo(statistics.BiggestMultiplier));
        });
    }

    [Test]
    public async Task ProcessMessage_InvalidJson_DoesNotThrowException()
    {
        string invalidJson = "invalid json";

        await SendMessage(invalidJson);
        await Task.Delay(100);

        Assert.That(_statisticsModel.Statistics, Is.Null.Or.EqualTo(new Playtech.Statistics.Statistics { ActivePlayers = 0, BiggestMultiplier = 0 }));
    }

    [Test]
    public async Task ProcessMessage_EmptyJson_UpdatesValuesToZero_DoesntThrowException()
    {
        var initialStatistics = new Playtech.Statistics.Statistics { ActivePlayers = 50, BiggestMultiplier = 2000 };
        _statisticsModel.GetType()?.GetProperty("Statistics")?.SetValue(_statisticsModel, initialStatistics);

        string emptyJson = "{}";
        await SendMessage(emptyJson);
        await Task.Delay(100);

        Assert.That(_statisticsModel.Statistics, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(_statisticsModel.Statistics.ActivePlayers, Is.EqualTo(0));
            Assert.That(_statisticsModel.Statistics.BiggestMultiplier, Is.EqualTo(0));
        });
    }

    [Test]
    public async Task ProcessMessage_WhitespaceJson_DoesntUpdate()
    {
        var initialStatistics = new Playtech.Statistics.Statistics { ActivePlayers = 50, BiggestMultiplier = 2000 };
        _statisticsModel.GetType()?.GetProperty("Statistics")?.SetValue(_statisticsModel, initialStatistics);

        string whitespaceJson = "  ";
        await SendMessage(whitespaceJson);
        await Task.Delay(100);

        Assert.That(_statisticsModel.Statistics, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(_statisticsModel.Statistics.ActivePlayers, Is.EqualTo(initialStatistics.ActivePlayers));
            Assert.That(_statisticsModel.Statistics.BiggestMultiplier, Is.EqualTo(initialStatistics.BiggestMultiplier));
        });
    }

    private async Task SendMessage(string message)
    {
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, _port);
            using var stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data);
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Socket exception: {ex.Message}");
            Assert.Fail($"Socket exception during SendMessage: {ex.Message}");
        }
    }

    private static int GetAvailablePort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }
}
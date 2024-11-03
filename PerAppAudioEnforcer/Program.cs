using System.IO.Pipes;
using System.Threading.Channels;

namespace PerAppAudioEnforcer;

internal static class Program
{
    private const string UniqueName = "AtgDev_PerAppAudioEnforcer";
    private static MainMenu menu;
    private static bool _isOnlyInstance;
    private static Mutex _mutex = new(true, UniqueName, out _isOnlyInstance);

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        if (!_isOnlyInstance)
        {
            MessageFirstInstance();
            return;
        }

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Init();
        _ = PipeServerLoop();
        Application.Run();
    }

    private static void Init()
    {
        var channel = Channel.CreateUnbounded<string>();
        Core c = new() { channelW = channel.Writer };
        _ = c.Start();

        menu = new MainMenu() { ChannelR = channel.Reader };
        var btn = menu.Controls.Find("ReloadConfigBtn", true).FirstOrDefault();
        if (btn is not null)
        {
            btn.Click += async (_, _) => await c.ReloadConfig().ConfigureAwait(false);
        }

        TrayIcon tray = new();
        tray.Open += (_, _) => menu.Show();
    }

    private static void MessageFirstInstance()
    {
        using NamedPipeClientStream client = new(".", UniqueName, PipeDirection.Out); // "." is for Local Computer
        try
        {
            client.Connect(1000);
            using StreamWriter w = new(client);
            w.Write("-open");
        }
        catch { }
    }

    private static async Task PipeServerLoop(CancellationToken ct = default)
    {
        await using NamedPipeServerStream server = new(UniqueName, PipeDirection.In);
        using StreamReader reader = new(server);
        while (!ct.IsCancellationRequested)
        {
            await server.WaitForConnectionAsync(ct);
            try
            {
                var str = await reader.ReadToEndAsync(ct);
                if (str != "-open") continue;

                menu.Show();
            }
            catch { }
            server.Disconnect();
        }
    }
}
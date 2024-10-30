using System.Threading.Channels;

namespace PerAppAudioEnforcer;

public partial class MainMenu : Form
{
    public required ChannelReader<string> ChannelR { get; init; }

    public MainMenu()
    {
        InitializeComponent();
        FormClosing += OnClose;
        Load += (_, _) => Task.Run(async () => await LogLoop());

    }

    private async ValueTask LogLoop()
    {
        await foreach (var m in ChannelR.ReadAllAsync())
        {
            LogBox.Invoke(() =>
            {
                LogBox.AppendText(m);
                LogBox.AppendText(Environment.NewLine);
            });
        }
    }

    private void OnClose(object? sender, FormClosingEventArgs e)
    {
        if (e.CloseReason != CloseReason.UserClosing) return;

        e.Cancel = true;
        Hide();
    }
}

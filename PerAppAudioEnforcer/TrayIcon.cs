using PerAppAudioEnforcer.Properties;

namespace PerAppAudioEnforcer;
public class TrayIcon : IDisposable
{
    private readonly NotifyIcon _notifyIcon = new()
    {
        Text = "Per App audio enforcer",
        Icon = Resources.MainIcon
    };
    private readonly ContextMenuStrip _contextMenu = new();

    private bool disposedValue;

    public TrayIcon()
    {
        Application.ApplicationExit += (_, _) => Dispose();
        AppDomain.CurrentDomain.ProcessExit += (_, _) => Dispose();

        _notifyIcon.Click += OnClick;
        _contextMenu.Items.Add(CreateOpenMenuButton());
        _contextMenu.Items.Add(CreateExitButton());
        _notifyIcon.ContextMenuStrip = _contextMenu;
        _notifyIcon.Visible = true;
    }

    private ToolStripMenuItem CreateOpenMenuButton()
    {
        ToolStripMenuItem item = new() { Text = "Open" };
        item.Font = new Font(item.Font, FontStyle.Bold);
        item.Click += OnOpenClick;
        return item;
    }

    private ToolStripMenuItem CreateExitButton()
    {
        ToolStripMenuItem item = new() { Text = "Exit" };
        item.Click += OnExitClick;
        return item;
    }

    private void OnOpenClick(object? sender, EventArgs e) => Open?.Invoke(sender, e);

    private void OnExitClick(object? sender, EventArgs e)
    {
        Dispose();
        Application.Exit();
    }

    private void OnClick(object? sender, EventArgs e)
    {
        if (e is not MouseEventArgs mouseE) return;
        if (mouseE.Button != MouseButtons.Left) return;

        Open?.Invoke(sender, e);
    }

    public EventHandler? Open;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            _contextMenu?.Dispose();
            _notifyIcon?.Dispose();
            disposedValue = true;
        }
    }

    ~TrayIcon()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

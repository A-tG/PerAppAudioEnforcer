using System.Diagnostics;
using System.Management;

namespace PerAppAudioEnforcer;
public class ProcessesWatcher : IDisposable
{
    private readonly ManagementEventWatcher _eventWatcher;
    private bool disposedValue;
    private ISet<(string, Action)> _appsActions = new HashSet<(string, Action)>(0);
    private bool IsRunning { get; set; } = false;

    public ISet<(string, Action)> AppsActions 
    { 
        set 
        {
            if (value is null) throw new ArgumentNullException(nameof(value));

            IsRunning = false;
            _appsActions = value;
            CheckAlreadyRunningApps();
            IsRunning = true;
        } 
    }

    public ProcessesWatcher()
    {
        EventQuery query = new("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance isa \"Win32_Process\"");
        _eventWatcher = new(query);
        _eventWatcher.EventArrived += OnEvent;
        _eventWatcher.Start();
    }

    private void CheckAlreadyRunningApps()
    {
        foreach (var (name, action) in _appsActions)
        {
            if (string.IsNullOrEmpty(name)) continue;

            try
            {
                if (!IsAppRunning(name)) continue;

                action();
            }
            catch { }
        }
    }

    private void OnEvent(object sender, EventArrivedEventArgs e)
    {
        if (!IsRunning) return;

        try
        {
            if (e.NewEvent.GetPropertyValue("TargetInstance") is not ManagementBaseObject instanceDescription) return;

            var name = Path.GetFileNameWithoutExtension(instanceDescription.GetPropertyValue("Name").ToString());
            if (string.IsNullOrEmpty(name)) return;

            var filteredAppsActions = _appsActions.Where(t => t.Item1 == name);
            if (!filteredAppsActions.Any()) return;

            var id = instanceDescription.GetPropertyValue("ParentProcessId").ToString();
            if (string.IsNullOrEmpty(id)) return;

            using var p = Process.GetProcessById(int.Parse(id));
            if (p.ProcessName == name) return;

            foreach (var (_, action) in filteredAppsActions)
            {
                action();
            }
        }
        catch { }
    }

    private static bool IsAppRunning(string appName)
    {
        var n = Path.GetFileNameWithoutExtension(appName);
        var processes = Process.GetProcessesByName(n);
        var isRunning = processes.Any(p => p.ProcessName == n);
        foreach (var p in processes)
        {
            p.Dispose();
        }
        return isRunning;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            _eventWatcher.Stop();
            _eventWatcher.Dispose();
            disposedValue = true;
        }
    }

    // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~ProcessesWatcher()
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

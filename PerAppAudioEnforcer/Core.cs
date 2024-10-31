using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace PerAppAudioEnforcer;
public class Core : IDisposable
{
    public ChannelWriter<string>? channelW;

    private string _perAudioSwitchProgramPath = "";
    private ProcessesWatcher? _watcher;

    private bool _hasStarted;

    public async Task Start()
    {
        if (_hasStarted) return;
        try
        {
            _watcher = new ProcessesWatcher()
            {
                AppsActions = await ReadAndProcessConfig()
            };
            _hasStarted = true;
        }
        catch (Exception e)
        {
            channelW?.TryWrite(e.Message);
        }
    }

    public async Task ReloadConfig()
    {
        try
        {
            if (_watcher is null)
            {
                channelW?.TryWrite("RELOAD");
                await Start();
                return;
            }

            channelW?.TryWrite("CONFIG RELOAD");
            _watcher.AppsActions = await ReadAndProcessConfig();
        }
        catch (Exception e)
        {
            channelW?.TryWrite(e.Message);
        }
    }

    private async Task<HashSet<(string, string, uint)>> ReadConfig()
    {
        const string config = "config.txt";

        HashSet<(string, string, uint)> result = [];
        if (!File.Exists(config)) throw new FileNotFoundException($"{config} not found");

        using var reader = File.OpenText(config);
        var ln = reader.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(ln)) throw new Exception("SoundVolumeView.exe path is not specified");
        if (!File.Exists(ln)) throw new Exception("Invalid SoundVolumeView.exe path");

        _perAudioSwitchProgramPath = ln;
        while ((ln = (await reader.ReadLineAsync())?.Trim()) is not null)
        {
            if (string.IsNullOrWhiteSpace(ln)) continue;

            ProcessConfigLine(result, ln);
        }

        return result;
    }

    private static void ProcessConfigLine(HashSet<(string, string, uint)> list, string ln)
    {
        StringBuilder sb = new(ln.Length);
        var isValidLine = !ln.StartsWith('"') && (ln.Count(ch => ch == '"') == 2);
        if (!isValidLine) throw new Exception($"Invalid config line: {ln}");

        var i = 0;
        for (; i < ln.Length; i++)
        {
            var ch = ln[i];
            if (ch == '"') break;

            sb.Append(ch);
        }
        var name = sb.ToString().Trim();

        i++;
        sb.Clear();
        for (; i < ln.Length; i++)
        {
            var ch = ln[i];
            if (ch == '"') break;

            sb.Append(ch);
        }
        var device = sb.ToString().Trim();
        if (string.IsNullOrEmpty(device)) throw new Exception($"Invalid config line: {ln}");

        uint delay = 0;
        ReadDelay(ln, i, ref delay);

        list.Add((name, device, delay));
    }

    private static void ReadDelay(string ln, int i, ref uint delay)
    {
        if (++i >= ln.Length) return;

        var numbStr = ln.Substring(i).Trim();
        if (numbStr.Length == 0) return;

        var split = numbStr.Split(' ');
        if (split.Length > 1)
        {
            numbStr = split[0];
        }

        if (uint.TryParse(numbStr, out uint n))
        {
            delay = n;
            return;
        }
        throw new Exception($"Error parsing Delay in '{ln}'");
    }

    private async Task<HashSet<(string, Action)>> ReadAndProcessConfig()
    {
        HashSet<(string, Action)> list = [];
        foreach (var (name, device, delay) in await ReadConfig())
        {
            list.Add((name, () =>
            {
                if (delay > 0)
                {
                    Task.Run(async () => {
                        await Task.Delay((int)delay);
                        TryStartSoundSwitchProgram(device, name);
                        channelW?.TryWrite($"{name}: applied");
                    });
                }
                else
                {
                    TryStartSoundSwitchProgram(device, name);
                }
                var delayMsg = delay > 0 ? $", delayed by {delay}ms" : "";
                channelW?.TryWrite(@$"{name}: applying ""{device}""{delayMsg}");
            }));
        }
        return list;
    }

    private void StartSoundSwitchProgram(string device, string app)
    {
        app = Path.GetFileNameWithoutExtension(app) + ".exe";
        using Process p = new()
        {
            StartInfo = new()
            {
                FileName = _perAudioSwitchProgramPath,
                Arguments = $@"{_perAudioSwitchProgramPath} /SetAppDefault ""{device}"" all ""{app}""",
                UseShellExecute = true
            }
        };
        p.Start();
    }

    private bool TryStartSoundSwitchProgram(string device, string app)
    {
        var res = false;
        try
        {
            StartSoundSwitchProgram(device, app);
            res = true;
        }
        catch (Exception e) 
        {
            channelW?.TryWrite(e.Message);
        }
        return res;
    }

    public void Dispose() => _watcher?.Dispose();
}

using System.Diagnostics;

namespace PerAppAudioEnforcer.Helper;
public static class OpenInOs
{
    public static bool TryOpen(string path)
    {
        bool result = false;
        try
        {
            Open(path);
            result = true;
        }
        catch { }
        return result;
    }

    public static void Open(string path)
    {
        using Process p = new()
        {
            StartInfo = new()
            {
                FileName = path,
                UseShellExecute = true
            }
        };
        p.Start();
    }
}

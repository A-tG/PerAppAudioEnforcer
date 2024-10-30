# Per App Audio Enforcer

"App volume and device preferences" are often get reset after Windows/Driver/Hardware upgrade. This program watch running processes and calls [SoundVolumeView.exe](https://www.nirsoft.net/utils/sound_volume_view.html) /SetAppDefault for specified in config programs, so reset does not matter anymore.

# config.txt example
```
C:\PATH\TO\SoundVolumeView.exe
msedge "Command-Line Friendly ID from SoundVolumeView of device"
```

## Donate to support the project
[Available methods](https://taplink.cc/atgdev)
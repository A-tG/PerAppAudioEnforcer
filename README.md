# Per App Audio Enforcer

"App volume and device preferences" are often got reset after Windows/Driver/Hardware upgrade. This program watch running processes and calls [SoundVolumeView.exe](https://www.nirsoft.net/utils/sound_volume_view.html) /SetAppDefault for specified in config programs, so reset does not matter anymore.

# config.txt example
Program name without .exe. Device id should be in quotation marks.
```
C:\PATH\TO\SoundVolumeView.exe
msedge "Command-Line Friendly ID from SoundVolumeView of device"
```
# How to get ID
1. Open SoundVolumeView
2. Double click on device

![image](https://github.com/user-attachments/assets/d9318ab4-17f0-4cef-b118-aa1c2e6f9641)

3. Copy this string

![image](https://github.com/user-attachments/assets/b0254bcc-840d-4829-9d40-31e1f8857eae)


## Donate to support the project
[Available methods](https://taplink.cc/atgdev)

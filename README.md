# Big Red Button

A Windows desktop utility that listens for a USB button press and triggers customizable actions, such as sending keypress shortcuts or running PowerShell scripts. Designed for automation, accessibility, and fun!

## Features
- Detects button press from supported USB device
- Triggers either a keypress shortcut or a PowerShell script
- Option to launch app at Windows startup
- Option to start app minimised to tray
- Simple Windows Forms UI

## Setup
1. **Requirements:**
   - Windows 10/11
   - .NET 8.0 Runtime
   - Supported USB button (default: Silicon Labs CP210x)
2. **Build & Run:**
   - Open in Visual Studio 2022+
   - Build and run the project
   - Or use `dotnet build` and `dotnet run` from the command line

## Usage
- Select the action type: Keypress or PowerShell Script
- For keypress, record or enter the shortcut (e.g. `CTRL + ALT + F`)
- For PowerShell, browse and select a `.ps1` script
- Use the checkboxes to enable launch at startup and/or start minimised
- When the USB button is pressed, the selected action is triggered
- The app can be minimised to the tray and restored by clicking the tray icon

## Contributing
This is a personal project and I am not accepting pull requests for it. Feel free to fork it if you find it useful though.

## License
MIT License. See [LICENSE](LICENSE) for details.

## Credits
- [System.IO.Ports](https://www.nuget.org/packages/System.IO.Ports/)
- [System.Management](https://www.nuget.org/packages/System.Management/)

---

*This project is not affiliated with Silicon Labs or any hardware vendor.*
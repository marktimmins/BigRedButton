using BigRedButton.Properties;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO.Ports;
using System.Management;

namespace BigRedButton
{
    /// <summary>
    /// Main application form for Big Red Button.
    /// Handles UI, device listening, and action triggering.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string DeviceId = @"USB\VID_10C4&PID_EA60\0001";
        private const string SignalMessage = "BUTTON_PRESS";

        private SerialPort? _serialPort;
        private bool _isRecording = false;

        // Key mappings for SendKeys
        private readonly Dictionary<string, string> _specialKeyMappings = new()
        {
            { "CTRL", "^" },
            { "ALT", "%" },
            { "SHIFT", "+" },

            { "BACK", "{BACKSPACE}" },
            { "CAPITAL", "{CAPSLOCK}" },
            { "NEXT", "{PGDN}" },
            { "PAGEUP", "{PGUP}" },

            { "D0", "{0}" },
            { "D1", "{1}" },
            { "D2", "{2}" },
            { "D3", "{3}" },
            { "D4", "{4}" },
            { "D5", "{5}" },
            { "D6", "{6}" },
            { "D7", "{7}" },
            { "D8", "{8}" },
            { "D9", "{9}" },
        };

        /// <summary>
        /// Initializes the main form, loads settings, and starts listening for device events.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
            if (Settings.Default.StartMinimised)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
            StartListening();
        }

        /// <summary>
        /// Loads persisted user settings into the UI.
        /// </summary>
        private void LoadSettings()
        {
            textBoxKeypress.Text = Settings.Default.KeypressShortcut;
            textBoxPowerShell.Text = Settings.Default.PowerShellScriptPath;
            if (Enum.TryParse(Settings.Default.SelectedActionType, out ActionType actionType))
            {
                radioKeypress.Checked = actionType == ActionType.Keypress;
                radioPowerShell.Checked = actionType == ActionType.PowerShellScript;
            }
            checkBoxLaunchOnStartup.Checked = Settings.Default.LaunchOnStartup;
            checkBoxStartMinimised.Checked = Settings.Default.StartMinimised;
        }

        /// <summary>
        /// Saves current UI state to user settings.
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.KeypressShortcut = textBoxKeypress.Text;
            Settings.Default.PowerShellScriptPath = textBoxPowerShell.Text;
            Settings.Default.SelectedActionType = GetSelectedActionType().ToString();
            Settings.Default.LaunchOnStartup = checkBoxLaunchOnStartup.Checked;
            Settings.Default.StartMinimised = checkBoxStartMinimised.Checked;
            Settings.Default.Save();
        }

        /// <summary>
        /// Gets the currently selected action type.
        /// </summary>
        private ActionType GetSelectedActionType()
        {
            if (radioKeypress.Checked) return ActionType.Keypress;
            if (radioPowerShell.Checked) return ActionType.PowerShellScript;
            return ActionType.None;
        }

        /// <summary>
        /// Triggers the selected action (keypress or PowerShell script).
        /// </summary>
        private void TriggerAction()
        {
            try
            {
                var actionType = GetSelectedActionType();
                switch (actionType)
                {
                    case ActionType.Keypress:
                        var parts = textBoxKeypress.Text.Split([" + "], StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
                        string shortcut = string.Empty;
                        foreach (var part in parts)
                        {
                            if (_specialKeyMappings.TryGetValue(part, out var specialKey))
                            {
                                shortcut += specialKey;
                            }
                            else if (part.Length == 1)
                            {
                                shortcut += "{" + part.ToLower() + "}";
                            }
                            else
                            {
                                shortcut += "{" + part.ToUpper() + "}";
                            }
                        }
                        SendKeys.SendWait(shortcut);
                        break;
                    case ActionType.PowerShellScript:
                        string scriptPath = textBoxPowerShell.Text;
                        if (!string.IsNullOrEmpty(scriptPath))
                        {
                            var psi = new ProcessStartInfo("powershell", $"-ExecutionPolicy Bypass -File \"{scriptPath}\"")
                            {
                                UseShellExecute = false
                            };
                            Process.Start(psi);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// Handles the record button click to start/stop shortcut recording.
        /// </summary>
        private void OnButtonRecordClick(object? sender, EventArgs e)
        {
            if (_isRecording)
            {
                // Stop recording
                _isRecording = false;
                buttonRecord.Text = "Record";
                KeyPreview = false;
                KeyUp -= OnFormKeyUp;
            }
            else
            {
                // Start recording
                _isRecording = true;
                buttonRecord.Text = "Stop Recording";
                textBoxKeypress.Text = string.Empty;
                textBoxKeypress.Focus();
                KeyPreview = true;
                KeyUp += OnFormKeyUp;
            }
        }

        /// <summary>
        /// Prevents Tab key from moving focus while recording.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_isRecording && keyData == Keys.Tab)
            {
                return true; // Do not process Tab key to avoid losing focus
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Handles key up events for shortcut recording.
        /// </summary>
        private void OnFormKeyUp(object? sender, KeyEventArgs e)
        {
            if (_isRecording)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    // Cancel recording
                    OnButtonRecordClick(sender, e);
                    return;
                }

                if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu)
                {
                    // Ignore modifier keys alone
                    return;
                }

                string keyString = string.Empty;
                if (e.Control) keyString += "CTRL + ";
                if (e.Alt) keyString += "ALT + ";
                if (e.Shift) keyString += "SHIFT + ";
                keyString += e.KeyCode.ToString();
                textBoxKeypress.Text = keyString;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles browse button click for PowerShell script selection.
        /// </summary>
        private void OnButtonBrowseClick(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "PowerShell Scripts (*.ps1)|*.ps1|All Files (*.*)|*.*";
            ofd.Title = "Select PowerShell Script";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxPowerShell.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// Starts listening for serial port data from the USB device.
        /// </summary>
        private void StartListening()
        {
            if (_serialPort == null || !_serialPort.IsOpen)
            {
                var portName = GetComPortName();
                if (!string.IsNullOrEmpty(portName))
                {
                    _serialPort = new SerialPort(portName, 9600);
                    _serialPort.DataReceived += OnSerialPortDataReceived;
                    try
                    {
                        _serialPort.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open port: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the COM port name for the supported USB device.
        /// </summary>
        private string GetComPortName()
        {
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");
            foreach (var obj in searcher.Get())
            {
                var id = obj["DeviceId"]?.ToString();
                if (DeviceId.Equals(id))
                {
                    var name = obj["Name"]?.ToString();
                    if (id != null && name != null)
                    {
                        // Extract COM port name (e.g., COM3)
                        var start = name.LastIndexOf("(COM");
                        if (start >= 0)
                        {
                            var end = name.IndexOf(')', start);
                            if (end > start)
                            {
                                return name.Substring(start + 1, end - start - 1); // e.g., COM3
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Handles serial port data received event.
        /// </summary>
        private void OnSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string? message = _serialPort?.ReadLine();
            if (message != null && message.Trim() == SignalMessage)
            {
                Invoke(new Action(TriggerAction));
            }
        }

        /// <summary>
        /// Handles the launch on startup checkbox change event.
        /// </summary>
        private void OnLaunchOnStartupCheckedChanged(object? sender, EventArgs e)
        {
            SetStartup(checkBoxLaunchOnStartup.Checked);
        }

        /// <summary>
        /// Adds or removes the app from Windows startup registry.
        /// </summary>
        private static void SetStartup(bool enable)
        {
            using var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key == null) return;

            string appName = "BigRedButton";
            if (enable)
            {
                key.SetValue(appName, '"' + Application.ExecutablePath + '"');
            }
            else
            {
                key.DeleteValue(appName, false);
            }
        }

        /// <summary>
        /// Handles form closing event to save settings and close serial port.
        /// </summary>
        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveSettings();
            _serialPort?.Close();
        }

        /// <summary>
        /// Handles form resize event to hide from taskbar when minimised.
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                ShowInTaskbar = false;
                SaveSettings();
            }
        }

        /// <summary>
        /// Handles mouse click on tray icon to restore the window.
        /// </summary>
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }
    }

    /// <summary>
    /// Supported action types for the button press.
    /// </summary>
    public enum ActionType
    {
        None,
        Keypress,
        PowerShellScript
    }
}

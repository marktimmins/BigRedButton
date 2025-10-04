using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;

namespace BigRedButton
{
    /// <summary>
    /// Application entry point for Big Red Button.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                EnsureCp210xDriverInstalled();
            }
            catch (Exception ex)
            {
                // Non-fatal: show a message but continue starting the app
                MessageBox.Show($"CP210X driver check/install failed: {ex.Message}", "Driver Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.Run(new MainForm());
        }

        /// <summary>
        /// Ensure CP210x driver (silabser.inf) is installed. If not present, extract embedded ZIP and install via pnputil.
        /// </summary>
        private static void EnsureCp210xDriverInstalled()
        {
            const string infFileName = "silabser.inf";
            // Check for pnputil presence by attempting to run it
            var enumOutput = RunProcessGetOutput("pnputil", "/enum-drivers");
            if (enumOutput != null && enumOutput.IndexOf(infFileName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Driver already in driver store
                return;
            }

            // Need to install from embedded ZIP resource
            var asm = Assembly.GetExecutingAssembly();
            var resourceName = asm.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("CP210x_Universal_Windows_Driver.zip", StringComparison.OrdinalIgnoreCase));
            if (resourceName == null)
            {
                throw new FileNotFoundException("Embedded CP210x driver ZIP resource not found.");
            }

            var tempDir = Path.Combine(Path.GetTempPath(), "BigRedButton_cp210x_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
            try
            {
                using (var stream = asm.GetManifestResourceStream(resourceName)!)
                using (var zip = new ZipArchive(stream))
                {
                    zip.ExtractToDirectory(tempDir);
                }

                // Look for the INF in the extracted root (user said INF in root of zip)
                var infPath = Path.Combine(tempDir, infFileName);
                if (!File.Exists(infPath))
                {
                    // Try search just in case
                    var found = Directory.GetFiles(tempDir, infFileName, SearchOption.AllDirectories).FirstOrDefault();
                    if (found != null)
                        infPath = found;
                }

                if (!File.Exists(infPath))
                    throw new FileNotFoundException($"Driver INF '{infFileName}' not found inside the embedded ZIP.");

                // Try installing non-elevated first
                var addArgs = $"/add-driver \"{infPath}\" /install";
                var addResult = RunProcessGetExitCodeAndOutput("pnputil", addArgs, out var output);
                if (addResult == 0)
                {
                    // success
                    return;
                }

                // If non-elevated failed, attempt elevated install using runas
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "pnputil",
                        Arguments = addArgs,
                        Verb = "runas",
                        UseShellExecute = true,
                        WorkingDirectory = tempDir
                    };
                    Process.Start(psi);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // User probably cancelled UAC or system prevented elevation
                    throw new UnauthorizedAccessException("Attempt to install CP210x driver requires administrator privileges. Please run the application as administrator or install the driver manually.");
                }
            }
            finally
            {
                // Do not delete immediately in case elevated process needs files; leave cleanup to OS temp or user.
            }
        }

        private static string? RunProcessGetOutput(string fileName, string arguments)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var p = Process.Start(psi);
                if (p == null) return null;
                var output = p.StandardOutput.ReadToEnd();
                var err = p.StandardError.ReadToEnd();
                p.WaitForExit(10000);
                return (output + "\n" + err);
            }
            catch
            {
                return null;
            }
        }

        private static int RunProcessGetExitCodeAndOutput(string fileName, string arguments, out string output)
        {
            output = string.Empty;
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var p = Process.Start(psi)!;
                output = p.StandardOutput.ReadToEnd() + "\n" + p.StandardError.ReadToEnd();
                p.WaitForExit(30000);
                return p.ExitCode;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return -1;
            }
        }
    }
}
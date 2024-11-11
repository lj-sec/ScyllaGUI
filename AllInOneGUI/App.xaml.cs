using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows;

namespace AllInOneGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string LogFile { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AdminRelauncher();

            int FileCount = 1;
            string logPath = @"C:\WindowsHardeningGUI\Logs";
            
            try
            {
                if(Directory.Exists(logPath))
                {
                    while (File.Exists(Path.Combine(logPath, $"log.{FileCount}.txt")))
                    {
                        FileCount++;
                    }
                }
                else
                {
                    Directory.CreateDirectory(logPath);
                }
                LogFile = Path.Combine(logPath, $"log.{FileCount}.txt");
                File.Create(LogFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error counting files: {ex.Message}");
            }

            void AdminRelauncher()
            {
                if (!IsRunAsAdmin())
                {
                    ProcessStartInfo proc = new ProcessStartInfo();
                    proc.UseShellExecute = true;
                    proc.WorkingDirectory = Environment.CurrentDirectory;
                    proc.FileName = Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe");

                    proc.Verb = "runas";

                    try
                    {
                        Process.Start(proc);
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("This program must be run as an administrator! \n\n" + ex.ToString());
                    }
                }
            }

            bool IsRunAsAdmin()
            {
                try
                {
                    WindowsIdentity id = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(id);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}

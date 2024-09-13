using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;

namespace AllInOneGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private static void ModifyRegistryKey(string registryPath, string valueName, int valueData)
        {
            try
            {
                // Open the registry key with write access
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath, writable: true))
                {
                    if (key == null)
                    {
                        Console.WriteLine($"Registry key '{registryPath}' not found.");
                        return;
                    }

                    // Set the registry value
                    key.SetValue(valueName, valueData, RegistryValueKind.DWord);
                    Console.WriteLine($"Successfully set '{valueName}' to {valueData} in '{registryPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while modifying the registry: {ex.Message}");
            }
        }

        private void HardeningClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmHardening = MessageBox.Show("Continue with General Windows Hardening? The following changes will be applied: https://github.com/c-u-r-s-e/AllInOneGUI/README.md", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmHardening == MessageBoxResult.Yes)
            {
                ModifyRegistryKey(@"HKLM\SYSTEM\CurrentControlSet\Control\Terminal Server", @"fDenyTSConnections",1);
            }
        }

        private void NewRuleClicked(object sender, RoutedEventArgs e)
        {
            NewRule newRule = new NewRule();
            newRule.Show();
        }

        private void RestartServicesClicked(object sender, RoutedEventArgs e)
        {
            RestartServices restartServices = new RestartServices();
            restartServices.Show();
        }

        private void SysinternalsClicked(object sender, RoutedEventArgs e)
        {
            string sysinternalsPath = @"C:\SysInternalsSuite";
            string zipPath = @"C:\SysInternalsSuite.zip";

            if(Directory.Exists(sysinternalsPath))
            {
                Sysinternals sysinternals = new Sysinternals();
                sysinternals.Show();
            }
            else if (File.Exists(zipPath))
            {
                MessageBoxResult confirmSysUnzip = MessageBox.Show("Unzip Sysinternals?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmSysUnzip == MessageBoxResult.Yes)
                {
                    ZipFile.ExtractToDirectory(zipPath, sysinternalsPath);
                }
            }
            else
            {
                MessageBoxResult confirmSysInstall = MessageBox.Show("Sysinternals is not detected, install now?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (confirmSysInstall == MessageBoxResult.Yes)
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile("https://download.sysinternals.com/files/SysinternalsSuite.zip", zipPath);
                    }
                }
            }
        }

        private void UpdateCSVClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}

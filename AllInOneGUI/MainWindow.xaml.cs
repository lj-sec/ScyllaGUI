using System.Windows;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Management.Automation;
using System.Reflection;

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

        // FIRST RUN FUNCTIONS

        private void HardeningClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmHardening = MessageBox.Show("Continue with General Windows Hardening? The following changes will be applied: https://github.com/c-u-r-s-e/AllInOneGUI/README.md", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (confirmHardening == MessageBoxResult.No)
            {
                return;
            }
            var app = Application.Current as App;
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                powerShellInstance.AddScript(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "GeneralHardening.ps1")));
                powerShellInstance.AddParameter($"logFile", $"{app.LogFile}");
                powerShellInstance.Invoke();
            }
        }

        private void NetClicked(object sender, RoutedEventArgs e)
        {

        }

        private void WmfClicked(object sender, RoutedEventArgs e)
        {

        }
        
        // FIREWALL FUCNTIONS

        private void EnableFirewallClicked(object sender, RoutedEventArgs e)
        {

        }

        private void FirewallKickClicked(object sender, RoutedEventArgs e)
        {

        }

        private void NewRuleClicked(object sender, RoutedEventArgs e)
        {
            NewRule newRule = new NewRule();
            newRule.Show();
        }

        private void DelModRuleClicked(object sender, RoutedEventArgs e)
        {

        }

        private void CodeRedClicked(object sender, RoutedEventArgs e)
        {

        }


        // ACTIVE DIRECTORY FUNCTIONS

        private void RemoveGroupClicked(object sender, RoutedEventArgs e)
        {

        }

        private void DefaultPassClicked(object sender, RoutedEventArgs e)
        {

        }

        // UPDATES FUNCTIONS

        private void AutoUpdateClicked(object sender, RoutedEventArgs e)
        {

        }

        private void AttemptFixClicked(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateCSVClicked(object sender, RoutedEventArgs e)
        {
            UpdateCSV updateCSV = new UpdateCSV();
            updateCSV.Show();
        }

        // GEN SEC FUNCTIONS

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

        private void WiresharkClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SnortClicked(object sender, RoutedEventArgs e)
        {

        }

        // INVENTORY FUNCTION

        private void InventoryClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}

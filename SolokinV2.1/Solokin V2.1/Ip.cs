using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solokin_V2._1
{
    class Ip
    {
        public static string IP = "";
       static async Task<string> GetCurrentPublicIp()
        {
            HttpClient httpClient = new HttpClient();
            string ipUrl = "https://api.ipify.org";

            HttpResponseMessage response = await httpClient.GetAsync(ipUrl);
            string ipAddress = await response.Content.ReadAsStringAsync();

            return ipAddress;
        }

        // Call the GetCurrentPublicIp() method to get the public IP address
       public static async void Get()
        {
            IP = await GetCurrentPublicIp();
        }


    }
}

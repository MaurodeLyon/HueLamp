using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HueLamp
{
    class NetworkHandler
    {
        private string ip;
        private string port;
        private string username;
        private string codedusername;
        private string allInfo;

        public NetworkHandler(string ip, string port, string username)
        {
            this.ip = ip;
            this.port = port;
            this.username = username;
            codedusername = "";
            getUsername();

        }

        //set lamp state
        private async void setLampState(string id, string state)
        {
            string data = "{\"on\":" + state + "}";
            await PutCommand("api/" + codedusername + "/lights/" + id + "/state", data);
        }

        //separate lamp state and lamp properties
        private async void setLamp(string id, string bri, string hue, string sat)
        {
            string data = "{\"bri\": " + bri + ", \"hue\": " + hue + ", \"sat\": " + sat + "  }";
            await PutCommand("api/" + codedusername + "/lights/" + id + "/state", data);
        }

        private async void getUsername()
        {
            string post = await PostCommand("api", "{\"devicetype\":\"MijnApp#{" + username + "}\"}");
            string[] data = post.Split('\"');
            codedusername = data[5];
            allInfo = await GetCommand("api/" + codedusername);
            string lamp = await getLamp("1");
        }

        public async Task<string> getLamp(string IdLamp)
        {
            return await GetCommand("api/" + codedusername + "/lights/" + IdLamp);
        }

        public async Task<string> GetCommand(string url)
        {
            url = "http://" + ip + ":" + port + "/" + url;
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); ;
            }
        }

        public async Task<string> PutCommand(string url, string Data)
        {
            url = "http://" + ip + ":" + port + "/" + url;
            HttpContent content = new StringContent(Data, Encoding.UTF8, "application/json");
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PostCommand(string url, string Data)
        {
            url = "http://" + ip + ":" + port + "/" + url;
            HttpContent content = new StringContent(Data, Encoding.UTF8, "application/json");
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HueLamp
{
    class NetworkHandler
    {
        private MainViewModel mainViewModel;
        private string ip;
        private string port;
        private string username;
        private string codedusername;
        private string allInfo;
        private int amountOfLamps;

        public NetworkHandler(string ip, string port, string username, MainViewModel mainViewModel)
        {
            this.ip = ip;
            this.port = port;
            this.username = username;
            codedusername = "";
            amountOfLamps = 0;
            this.mainViewModel = mainViewModel;
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
            getAllInfo();
        }

        private async void getAllInfo()
        {
            allInfo = await GetCommand("api/" + codedusername);
            getAmountOfLamps();
        }

        private async void getAmountOfLamps()
        {
            string lampInfo;
            string[] list;
            int n = 1;
            do
            {
                amountOfLamps++;
                lampInfo = await getLamp(n.ToString());
                list = lampInfo.Split('"');
                mainViewModel.Lamps.Add(new Lamp(list[4],list[6],list[8],list[10]));
                n++;
            }
            while (list[1] != "error");
            amountOfLamps--;
            mainViewModel.Lamps.RemoveAt(mainViewModel.Lamps.Count - 1);
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

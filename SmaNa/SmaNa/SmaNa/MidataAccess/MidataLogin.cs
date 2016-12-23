using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ModernHttpClient;
using System.Net.Http;

namespace SmaNa.MidataAccess
{
    class MidataLogin
    {
        private static readonly string appname = "SmaNa";
        private static readonly string secret = "BuNHh98HVvLi7AuY";
        private static readonly string username = "smana@midata.coop";
        private static readonly string password = "Smana123456";
        private static readonly string role = "member";
        private static readonly string endpoint = "https://test.midata.coop:9000";

        private string _loginToken;
        private string _refreshToken;

        private HttpClient client;
        public MidataLogin()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

        }

        public async void Login()
        {
            AuthRequest ar = new AuthRequest() { appname = appname, username = username, password = password, secret = secret, role = role };
            var data = JsonConvert.SerializeObject(ar);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri(endpoint + "/v1/auth");
            
            var response = await client.PostAsync(uri,content);

            if (response.IsSuccessStatusCode)
            {
                var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content.ReadAsStringAsync().Result);
                _loginToken = authResponse.authToken;
                _refreshToken = authResponse.refreshToken;
            }
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace SmaNa.MidataAccess
{
    /// <summary>
    /// This class is used to Access Midata, as well as get and store there a Bodyweight. 
    /// </summary>
    public class MidataLogin
    {
        /// <summary>
        /// All data needed to make a Login on test.midata.coop
        /// </summary>
        private static readonly string appname = "SmaNa";
        private static readonly string secret = "BuNHh98HVvLi7AuY";
        private static readonly string username = "smana@midata.coop";
        private static readonly string password = "Smana123456";
        private static readonly string role = "member";
        // we actually only connect to the testenvironment
        private static readonly string endpoint = "https://test.midata.coop:9000";


        private string _loginToken;
        private string _refreshToken;

        private HttpClient client;

        public MidataLogin()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

        }

        /// <summary>
        /// Asynchronously starts a Midata-Session 
        /// </summary>
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
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResponse.authToken);
                _loginToken = authResponse.authToken;
                _refreshToken = authResponse.refreshToken;
            }
        }

        /// <summary>
        /// Saves the bw to Midata. You should be logged in before you use this method.
        /// </summary>
        /// <param name="bw">The BodyWeight to be stored</param>
        public async void SaveWeight(BodyWeight bw)
        {
            if(_loginToken == null)
            {
                await Task.Delay(1000);
            }
            //var message = new FHIRMessage() { Content = bw };
            var data = JsonConvert.SerializeObject(bw);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var uri = new Uri(endpoint + "/fhir/Observation");
            var response = await client.PostAsync(uri, content);
        }
        /// <summary>
        /// Gets all BodyWeights from Midata and returns the latest one. You should be logged in before you use this method.
        /// </summary>
        /// <returns>The last (effectiveDateTime) stored BodyWeight on Midata</returns>
        public async Task<BodyWeight> getLastWeight()
        {
            var uri = new Uri(endpoint + "/fhir/Observation?code=3141-9");
            var get = await client.GetAsync(uri);
            var result = JsonConvert.DeserializeObject<Response>(get.Content.ReadAsStringAsync().Result);
            return result.entry.OrderByDescending(x=>x.resource.effectiveDateTime).FirstOrDefault().resource;
        }
    }
}

using System.Net.Http.Headers;
using System.Text;
using INVEST.BUSINESSLOGIC.Settings;
using INVEST.SITE.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace INVEST.SITE.Service
{
    public class RestService : IRestService
    {
        private readonly AppSettings _settings;
        private static TimeSpan clientTimeOut = TimeSpan.FromMinutes(60);

        public RestService(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> ExecuteGetAsync(string url, string accept = "application/json")
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.Timeout = clientTimeOut;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            var endpoint = string.Concat(_settings.GetAppBaseApiUrl(), url);

            var response = await httpClient.GetAsync(endpoint);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ExecutePostAsync(string url, object payload, string accept = "application/json")
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.Timeout = clientTimeOut;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "123456");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver(),
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };

            var endpoint = string.Concat(_settings.GetAppBaseApiUrl(), url);

            var response = await httpClient.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(payload, settings), Encoding.UTF8, accept));

            return await response.Content.ReadAsStringAsync();
        }

    }
}

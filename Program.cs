using System;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace Doppler.Examples
{
    public class Doppler
    {
        [JsonProperty("DOPPLER_PROJECT")]
        public string DopplerProject { get; set; }

        [JsonProperty("DOPPLER_ENVIRONMENT")]
        public string DopplerEnvironment { get; set; }

        [JsonProperty("DOPPLER_CONFIG")]
        public string DopplerConfig { get; set; }

        public static Doppler FetchSecrets()
        {
            var DOPPLER_TOKEN = Environment.GetEnvironmentVariable("DOPPLER_TOKEN");
            var client = new RestClient("https://api.doppler.com/v3/configs/config/secrets/download?format=json");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes(DOPPLER_TOKEN + ":"))}");
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Doppler>(response.Content);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var secrets = Doppler.FetchSecrets();
            Console.WriteLine($"Project: {secrets.DopplerProject}");
            Console.WriteLine($"Environment: {secrets.DopplerEnvironment}");
            Console.WriteLine($"Config: {secrets.DopplerConfig}");
        }
    }
}
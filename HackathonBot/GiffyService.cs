using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace HackathonBot
{
    public class GiffyService
    {
        private readonly HttpClient _client;

        private const string Url = "http://api.giphy.com/v1/gifs/random";
        private const string Query = "?rating=g&tag=";
        private const string ApiKey = "75e38046ebae4975980123dbca15edb7";

        public GiffyService()
        {
            _client = new HttpClient();
        }

        public string GetRandomGif(string tag)
        {
            var giffyUrl = Url + Query + tag;

            _client.DefaultRequestHeaders.Add("api_key", ApiKey);

            var response = _client.GetAsync(giffyUrl).Result;
            var json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            var gif = json["data"]["image_original_url"];

            return gif.ToString();
        }
    }
}
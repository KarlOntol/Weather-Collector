using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherCollector
{
    public static class WeatherCollector
    {
        static readonly HttpClient client;
        private const string APIkey = "61f711a6bffc0f6797e20053019ca010";

        static WeatherCollector()
        {
            client = new HttpClient();
        }

        public static async Task GetData(float lat, float lon, string lang)
        {
            try
            {
                string url;
                url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&APPID={APIkey}&exclude=hourly,daily&units=metric&lang={lang}";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Print(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Ошибка запроса страницы");
            }
        }

        public static void Print(string json)
        {
            JObject jObject = JObject.Parse(json);
            var name = jObject["name"];
            var temp = jObject["main"]["temp"];
            JToken token = jObject["weather"];
            var descList = token.ToObject<List<JObject>>();
            var desc = descList[0]["description"];
            Console.WriteLine($"{DateTime.Now:f}; {name}; {temp} градусов по Цельсию ({desc})");
        }
    }
}
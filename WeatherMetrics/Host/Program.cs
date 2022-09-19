using WeatherCollector;

namespace WeatherMetrics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await WeatherCollector.WeatherCollector.GetData(53.8983f, 27.5532f, "ru");
        }
    }
}

using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Weather;

namespace WeatherConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            Console.WriteLine("Got Channel");
            var client = new WeatherService.WeatherServiceClient(channel);
            Console.WriteLine("calling GetWeather...");
            var weatherResponse = await client.GetWeatherAsync(new WeatherRequest());

            foreach (var item in weatherResponse.Items)
            {
                Console.WriteLine($"{item.TempC } {item.Syummary} ");
            }
            
        }
    }
}

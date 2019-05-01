using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RealTimeAnalyzer.Publisher.ErrorLogging.Config;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeAnalyzer.Publisher.ErrorLogging
{
    static class Program
    {
        static IConfiguration _config;
        static EventHubConfig _eventHubConfig;
        static StorageAccountConfig _storageAccountConfig;
        static EventHubClient _eventHubClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Initializing application!!!");
            Init();

            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var transactionsTask = Task.Run(async delegate
            {
                Console.WriteLine("Transactions task started!!!");
                while (!cts.IsCancellationRequested)
                {
                    int timeout = new Random().Next(1, 50);
                    var msg = CreateTransaction();
                    await Task.Delay(timeout);
                    Publish(msg);

                    Console.WriteLine($"Message sent: {msg.ToString()}");
                }
            });

            Console.WriteLine("Press ESC to terminate the client");
            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("Publisher stopped!!!");
            Console.ReadKey();

        }

        static Transaction CreateTransaction()
        {
            Location loc = RandomizeLocation();

            Transaction trn = new Transaction()
            {
                AppName = RandomizeText("Application", 10),
                Region = loc.Region,
                Account = RandomizeText("Account", 100),
                Amount = RandomizeAmount(),
                Created = RandomizeDate()
            };

            return trn;
        }

        static decimal RandomizeAmount()
        {
            Random x = new Random();
            return (decimal)Math.Round(x.NextDouble() * 1000, 2);
        }

        static Location RandomizeLocation()
        {
            City city = (City)new Random().Next(0, 20);
            switch (city)
            {
                case City.Bluff:
                    return new Location() { Latitude = -46.6, Longitude = 168.33333, Region = city.ToString() };
                case City.Invercargill:
                    return new Location() { Latitude = -46.41627, Longitude = 168.26667, Region = city.ToString() };
                case City.Queenstown:
                    return new Location() { Latitude = -45.0281, Longitude = 168.73941, Region = city.ToString() };
                case City.Christhchurch:
                    return new Location() { Latitude = -43.53, Longitude = 172.62028, Region = city.ToString() };
                case City.Nelson:
                    return new Location() { Latitude = -41.29306, Longitude = 173.23806, Region = city.ToString() };
                case City.Blenheim:
                    return new Location() { Latitude = -41.56761, Longitude = 173.93759, Region = city.ToString() };
                case City.Dunedin:
                    return new Location() { Latitude = -45.86667, Longitude = 170.5, Region = city.ToString() };
                case City.Wellington:
                    return new Location() { Latitude = -41.28889, Longitude = 174.77722, Region = city.ToString() };
                case City.Napier:
                    return new Location() { Latitude = -39.466488, Longitude = 176.87204, Region = city.ToString() };
                case City.NewPlymouth:
                    return new Location() { Latitude = -39.0097, Longitude = 174.179, Region = city.ToString() };
                case City.Tauranga:
                    return new Location() { Latitude = -37.671043, Longitude = 176.19751, Region = city.ToString() };
                case City.Hamilton:
                    return new Location() { Latitude = -37.78333, Longitude = 175.28333, Region = city.ToString() };
                case City.Auckland:
                    return new Location() { Latitude = -36.85, Longitude = 174.78333, Region = city.ToString() };
                case City.HobsonvillePoint:
                    return new Location() { Latitude = -36.789192, Longitude = 174.67136, Region = city.ToString() };
                case City.ASBCDrive:
                    return new Location() { Latitude = -36.733711, Longitude = 174.712771, Region = city.ToString() };
                case City.Whangarei:
                    return new Location() { Latitude = -35.76727, Longitude = 174.3647, Region = city.ToString() };
                case City.TeHapua:
                    return new Location() { Latitude = -34.5184, Longitude = 172.9085, Region = city.ToString() };
                case City.SaoPaulo:
                    return new Location() { Latitude = -23.538111, Longitude = -46.57543, Region = city.ToString() };
                case City.Virginia:
                    return new Location() { Latitude = 38.8180576, Longitude = -77.4203773, Region = city.ToString() };
                case City.London:
                    return new Location() { Latitude = 51.50722, Longitude = -0.1275, Region = city.ToString() };
                default:
                    throw new Exception("Error calculating location");
            }
        }

        static string RandomizeText(string text, int options)
        {
            Random x = new Random();
            return $"{text} {x.Next(1, options)}";
        }

        static string RandomizeAppType()
        {
            int appType = new Random().Next(2);
            if (appType == 1)
                return "Mobile";
            else
                return "Browser";
        }

        static DateTime RandomizeDate()
        {
            Random x = new Random();
            return new DateTime(2019, x.Next(3, 5), x.Next(1, 30), 0, 0, 0);
        }

        static void Publish<T>(T message)
        {
            // 1. Serialize the message
            var serializedMessage = JsonConvert.SerializeObject(message);

            // 2. Convert serialized message to bytes
            var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);

            // 3. Wrap message bytes in EventData instance
            var eventData = new EventData(messageBytes);

            // 4. Publish the event
            _eventHubClient.SendAsync(eventData);
        }

        #region Initialization
        static void Init()
        {
            InitConfig();
            InitEventHub();
        }

        static void InitConfig()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("Config\\appsettings.json", true, true)
                .Build();
            _eventHubConfig = _config.GetSection("EventHub").Get<EventHubConfig>();
            _storageAccountConfig = _config.GetSection("StorageAccount").Get<StorageAccountConfig>();
        }

        static void InitEventHub()
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(_eventHubConfig.ConnectionString);
        }
        #endregion
    }
}

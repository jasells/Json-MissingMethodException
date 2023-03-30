using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Debug = System.Diagnostics.Debug;

namespace Json.Error.Demo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            JsonStuff();

            MainPage = new MainPage();
        }

        public static void JsonStuff()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot",
                SummaryField = "Hot",
                DatesAvailable = new List<DateTimeOffset>()
                    { DateTime.Parse("2019-08-01"), DateTime.Parse("2019-08-02") },
                TemperatureRanges = new Dictionary<string, HighLowTemps>
                {
                    ["Cold"] = new HighLowTemps { High = 20, Low = -10 },
                    ["Hot"] = new HighLowTemps { High = 60, Low = 20 }
                },
                SummaryWords = new[] { "Cool", "Windy", "Humid" }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };

            //options.Encoder = System.Text.UTF8Encoding.UTF8;
            string jsonString = JsonSerializer.Serialize(weatherForecast, options);
            Debug.WriteLine(jsonString);
            Console.WriteLine(jsonString);


            // streamed serialized
            // requires .net stnd 2.1+
            var bwriter = new ArrayBufferWriter<byte>(1024);

            var jw = new Utf8JsonWriter(bwriter);

            JsonSerializer.Serialize(jw, weatherForecast, options);

            var jsonUtf8 = System.Text.ASCIIEncoding.UTF8.GetString(bwriter.WrittenSpan);

            Debug.WriteLine("UTF encoded-----------------------------------------");
            Debug.WriteLine(jsonUtf8);
            Console.WriteLine("UTF encoded-----------------------------------------");
            Console.WriteLine(jsonUtf8);

            var money = -528.49m;
            Debug.WriteLine($"negative money, expected: -$528.489, actual: {money:C2}");
            // de-serialize

            var utfReader = new Utf8JsonReader(bwriter.WrittenSpan);

            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

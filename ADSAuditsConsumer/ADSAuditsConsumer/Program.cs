using ADSAuditsConsumer.DALL.Model;
using ADSAuditsConsumer.Model;
using ADSAuditsConsumer.Utils;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace ADSAuditsConsumer
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string connectionString = "mongodb://mongoadmin:asegsys@172.18.0.6:27017";

                // Create a MongoClient object by using the connection string
                var client = new MongoClient(connectionString);

                //Use the MongoClient to access the server
                var database = client.GetDatabase("log");
                var collection = database.GetCollection<LogModel>("Logs");


                var bookingStream = new BookingStream();
                var bookingConsumer = new BookingConsumer(bookingStream, Console.WriteLine);
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var consumerName = Environment.GetEnvironmentVariable("CONSUMER_NAME");

                consumerName = string.IsNullOrWhiteSpace(consumerName)
                                ? $"DinamicConsumer{DateTime.Now.Ticks}" :
                                consumerName;

                Console.WriteLine($"ConsumerName : {consumerName}");

                bookingStream.Subscribe(consumerName, (m) =>
                {
                    Console.WriteLine($"Log : {m.Message}");

                    try
                    {
                        var entrada = JsonConvert.DeserializeObject<LogModelIn>(m.Message);
                        Console.WriteLine(m.Message);


                        var oJson = JObject.Parse(JsonConvert.SerializeObject(entrada.Cuerpo));
                        var entity = new LogModel
                        {
                            Application = entrada.Application,
                            Cuerpo = oJson.ToString(),
                            LogLevel = entrada.LogLevel,
                            TransactionId = entrada.TransactionId,
                        };

                        collection.InsertOne(entity);

                        var id = entity.Id;

                        Console.WriteLine("Id=" + id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                });


                bookingConsumer.Listen();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

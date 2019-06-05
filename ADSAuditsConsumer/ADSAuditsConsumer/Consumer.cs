using ADSAuditsConsumer.DALL.Model;
using Confluent.Kafka;

using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ADSAuditsConsumer
{
    public class Consumer
    {
        public Consumer()
        {          
        }

        public void Listen()
        {
            var host = Environment.GetEnvironmentVariable("MongoDB__Host");
            var database = Environment.GetEnvironmentVariable("MongoDB__Database");
            var user = Environment.GetEnvironmentVariable("MongoDB__User");
            var password = Environment.GetEnvironmentVariable("MongoDB__Password");

            var connectionString = "";

            if (string.IsNullOrWhiteSpace(user)
                || string.IsNullOrWhiteSpace(password))
            {
                connectionString = $"mongodb://{host}";
            }
            else
            {
                connectionString = $"mongodb://{user}:{password}@{host}";
            }            

            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            var db = client.GetDatabase(database);
            var collection = db.GetCollection<LogModel>("Logs");

            var consumerName = Environment.GetEnvironmentVariable("CONSUMER_NAME");

            consumerName = string.IsNullOrWhiteSpace(consumerName)
                           ? $"DinamicConsumer{DateTime.Now.Ticks}" :
                           consumerName;

            Console.WriteLine($"ConsumerName : {consumerName}");

            var kafkaService = Environment.GetEnvironmentVariable("Global__Services__kafka__Service");

            var config = new ConsumerConfig
            {
                GroupId = consumerName,
                BootstrapServers = kafkaService,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe("ADSLoggerTopic");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var m = c.Consume(cts.Token);

                            var log = JsonConvert.DeserializeObject<LogModel>(m.Value);

                            Console.WriteLine(m.Value);                         

                            collection.InsertOne(log);

                            var id = log.Id;

                            Console.WriteLine("Id=" + id);
                            
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {                    
                    c.Close();
                }
            }
        }
    }
}
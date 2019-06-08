using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ADSUtilities.Logger
{
    public class ADSUtilitiesLoggerProducer: IDisposable
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<Null, string> _producer;
        private readonly string _topicName;
        public static string KafkaServer;        

        public ADSUtilitiesLoggerProducer()
        {
            Console.WriteLine($"Conecting kafka {KafkaServer}...");

            try
            {

                _config = new ProducerConfig
                {
                    BootstrapServers = KafkaServer // "localhost:9092"
                };

                _producer = new ProducerBuilder<Null, string>(_config)
                                    .Build();

                _topicName = "ADSLoggerTopic";

                Console.WriteLine($"Conecting kafka {KafkaServer}...OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Conecting kafka {KafkaServer}...ERROR");
                Console.WriteLine(ex.Message);
            }
            
        }

        public void WriteMessage(Object o)
        {
            var thread = new Thread(p => {

                var message = JsonConvert.SerializeObject(o);

                try
                {
                    Console.WriteLine("Sending kafka...");
                    Console.WriteLine(message);

                    _producer
                        .Produce(_topicName, new Message<Null, string> { Value = message },
                            r => {

                                if (!r.Error.IsError)
                                {
                                    Console.WriteLine($"Delivered message to {r.TopicPartitionOffset}");
                                }
                                else
                                {
                                    Console.WriteLine($"Delivery Error: {r.Error.Reason}");
                                }
                            }
                        );                    

                    //Console.WriteLine("Flush, {0}", message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error.");
                    Console.WriteLine(ex.Message);
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }

        #region IDisposable Support

        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _producer.Flush(TimeSpan.FromSeconds(10));
                    _producer.Dispose();
                }
                disposedValue = true;
            }
        }

        ~ADSUtilitiesLoggerProducer()
        {
            Dispose(false);
        }
        public void Dispose()
        {         
            Dispose(true);         
        }

        #endregion

    }
}

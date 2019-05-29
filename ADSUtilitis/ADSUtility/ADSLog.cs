using Confluent.Kafka;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Confluent.Kafka.Serialization;
using System.Text;
using Newtonsoft.Json;
using ADSUtilities.Util;

namespace ADSLog
{
    public class Log
    {
        private string _topicName;
        private readonly Producer<Null, string> _producer;
        private static readonly Random rand = new Random();
        private LogLevel level;        

        public Log()
        {
            level = new LogLevel {
                Critical = "critical",
                Error = "error",
                Info = "info",
                Warning = "warning"
            };
            this._topicName = "logstopic";

            var config = new Dictionary<string, object> {
                {"bootstrap.servers", "kafka:9092"}
            };

            _producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));            
        }
        private async Task WriteMessage(Object o)
        {
            var message = JsonConvert.SerializeObject(o);
            try
            {                
                _ = _producer.ProduceAsync(this._topicName, null, message);
                _producer.Flush(10);

                Console.WriteLine("Flush, {0}", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                    
        }

        public async void Error(string parMesagge, object parObj, string TransactionId = null)
        {
            var error = new LogStructure
            {
                LogLevel = level.Error,
                Application = "app",
                Cuerpo = parObj,
                TransactionId = TransactionId,
                Mesagge = parMesagge,
            };
            await this.WriteMessage(error);
        }

        public async void Warning(string parMesagge, Object parObj, string TransactionId = null)
        {
            var error = new LogStructure
            {
                LogLevel = level.Warning,
                Application = "app",
                Cuerpo = parObj,
                TransactionId = TransactionId,
                Mesagge = parMesagge
            };
            await this.WriteMessage(error);
        }
        public async  void Info(string parMesagge, Object parObj, string TransactionId = null)
        {
            var error = new LogStructure
            {
                LogLevel = level.Error,
                Application = "app",
                Cuerpo = parObj,
                TransactionId = TransactionId,
                Mesagge = parMesagge
            };
            await this.WriteMessage(error);
        }        
    }
}

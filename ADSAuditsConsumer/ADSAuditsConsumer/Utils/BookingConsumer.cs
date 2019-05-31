using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSAuditsConsumer.Utils
{
    public class BookingConsumer : IBookingConsumer
    {
        private readonly IBookingStream bookingStream;
        private readonly Action<string> logger;

        public BookingConsumer(IBookingStream bookingStream, Action<string> logger)
        {
            this.bookingStream = bookingStream;
            this.logger = logger;
        }

        public void Listen()
        {
            var config = new Dictionary<string, object>
            {
                {"group.id","booking_consumer" },
                {"bootstrap.servers", "kafka:9092" },
                { "enable.auto.commit", "false" }
            };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Subscribe("logstopic");
                logger("Subscribed");

                consumer.OnMessage += (_, msg) => {
                    bookingStream.Publish(new BookingMessage { Message = msg.Value });
                };
                logger("OnMessage attached");

                while (true)
                {
                    consumer.Poll(100);
                }
            }
        }
    }
}
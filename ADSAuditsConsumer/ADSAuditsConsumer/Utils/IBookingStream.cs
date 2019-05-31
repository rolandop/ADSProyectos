using System;
using System.Collections.Generic;
using System.Text;

namespace ADSAuditsConsumer.Utils
{
    public interface IBookingStream
    {
        void Publish(BookingMessage bookingMessage);
        void Subscribe(string subscriberName, Action<BookingMessage> action);
    }
}

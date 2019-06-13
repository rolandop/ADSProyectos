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
                
                var consumer = new Consumer();
                consumer.Listen();

            }
            catch (Exception ex)
            {
                Console.Write("ERROR: ");
                Console.Write(ex.Message);
            }            
        }
    }
}

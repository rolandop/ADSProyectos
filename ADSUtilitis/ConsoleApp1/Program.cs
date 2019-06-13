
using ADSLog;
using System;

namespace ConsoleApp1
{
    public class Persona
    {
        public int Edad { get; set; }
        public String Nombre { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Persona();
            p.Edad = 11;
            p.Nombre = "Pepe";
            var log = new Log();

            for (var i = 0; i <= 100; i++)
            {
                log.Warning("error", p, i.ToString());
                Console.WriteLine("Send" + i.ToString());
               
            }

            Console.WriteLine("ok");
        }
    }

}

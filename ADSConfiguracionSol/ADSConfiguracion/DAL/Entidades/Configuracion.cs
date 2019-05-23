
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ADSConfiguracion.DAL.Entidades
{
    public class Configuracion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("ServicioId")]
        public string ServicioId { get; set; }        

        [BsonElement("ServicioVersion")]
        public string ServicioVersion { get; set; }

        [BsonElement("Ambiente")]
        public string Ambiente { get; set; }

        [BsonElement("Seccion")]
        public string Seccion { get; set; }

        [BsonElement("Clave")]
        public string Clave { get; set; }

        [BsonElement("Valor")]
        public string Valor { get; set; }

        [BsonElement("Descripcion")]
        public string Descripcion { get; set; }

        [BsonDateTimeOptions]        
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

    }
}

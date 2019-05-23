
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ADSConfiguracion.DAL.Entidades
{
    public class Servicio
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

        [BsonElement("UrlActualizacion")]
        public string UrlActualizacion { get; set; }

        [BsonElement("UrlVerificacion")]
        public string UrlVerificacion { get; internal set; }

        [BsonElement("Activo")]
        public bool Activo { get; set; } = true;

        [BsonDateTimeOptions]
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        [BsonDateTimeOptions]
        public DateTime FechaVerificacion { get; set; } = DateTime.Now;

        [BsonDateTimeOptions]
        public DateTime FechaBaja { get; set; }

        [BsonElement]
        public int Intentos { get; set; }

    }
}

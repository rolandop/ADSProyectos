
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ADSConfiguracion.DAL.Entities
{
    public class Service
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("ServiceId")]
        public string ServiceId { get; set; }        

        [BsonElement("ServiceVersion")]
        public string ServiceVersion { get; set; }

        [BsonElement("Environment")]
        public string Environment { get; set; }

        [BsonElement("UpdateUrl")]
        public string UpdateUrl { get; set; }

        [BsonElement("VerifyUrl")]
        public string VerifyUrl { get;  set; }

        [BsonElement("Active")]
        public bool Active { get; set; } = true;

        [BsonDateTimeOptions]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [BsonDateTimeOptions]
        public DateTime VerifyDate { get; set; } = DateTime.Now;

        [BsonDateTimeOptions]
        public DateTime UnsubscribeDate { get; set; }

        [BsonElement]
        public int Attempts { get; set; }

    }
}

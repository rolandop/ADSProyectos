
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ADSConfiguracion.DAL.Entities
{
    public class Configuration
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

        [BsonElement("Section")]
        public string Section { get; set; }

        [BsonElement("Key")]
        public string Key { get; set; }

        [BsonElement("Value")]
        public string Value { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonDateTimeOptions]        
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}

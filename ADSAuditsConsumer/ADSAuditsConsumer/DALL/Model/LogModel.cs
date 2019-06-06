﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSAuditsConsumer.DALL.Model
{

    public class LogModel
    {
        [BsonId]
        // standard BSonId generated by MongoDb
        public ObjectId Id { get; set; }
        public string TraceId { get; set; }
        public string Service { get; set; }      
        public int EventId { get; set; }
        public object LogDetail { get; set; }
        public string Message { get; set; }      
        public string LogLevel { get; set; }

        [BsonDateTimeOptions]        
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
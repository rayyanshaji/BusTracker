using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusTracker.Models
{
    public class Location
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("LocationID")]
        public int LocationID { get; set; }

        [BsonElement("OriginAddress")]
        public string OriginAddress { get; set; }

        [BsonElement("DestinationAddress")]
        public string DestinationAddress { get; set; }
    }
}

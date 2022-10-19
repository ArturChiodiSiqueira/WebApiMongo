﻿using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongo.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }
}

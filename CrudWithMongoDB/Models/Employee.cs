using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CrudWithMongoDB.Models
{
    public class Employee
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    }
}

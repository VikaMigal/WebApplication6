
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication6.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Login")]
        public string Login { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
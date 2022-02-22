using CrimeApi.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CrimeApi.Models
{
    public class CrimeEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public string Localization { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public EventStatus Status { get; set; }
        public int? LawEnforcementId {get; set; }
    }
}

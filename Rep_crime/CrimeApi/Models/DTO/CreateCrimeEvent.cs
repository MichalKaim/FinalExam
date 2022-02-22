using CrimeApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CrimeApi.Models.DTO
{
    public class CreateCrimeEvent
    {
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public string Localization { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public EventStatus Status { get; set; }
    }
}

using LawEnforcementApi.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawEnforcementApi.Model
{
    public class LawEnforcement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Rank Rank { get; set; }
        public IList<Event>? Events { get; set; }
    }
}

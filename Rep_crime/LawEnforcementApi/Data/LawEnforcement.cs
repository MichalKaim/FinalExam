using LawEnforcementApi.Data.Enums;

namespace LawEnforcementApi.Data
{
    public class LawEnforcement
    {
        public int Id { get; set; }
        public Rank Rank { get; set; }
        public List<string> events { get; set; }
    }
}

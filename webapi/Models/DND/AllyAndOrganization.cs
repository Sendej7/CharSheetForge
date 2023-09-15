namespace webapi.Models.DND
{
    public class AllyAndOrganization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Ally, Organization, etc.
        public string Description { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;// How is the character related?
        public int TrustLevel { get; set; } // A quantifiable measure of trust or standing
    }
}

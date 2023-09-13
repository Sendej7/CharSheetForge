namespace webapi.Models.DND
{
    public class AllyAndOrganization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Ally, Organization, etc.
        public string Description { get; set; }
        public string Relationship { get; set; } // How is the character related?
        public int TrustLevel { get; set; } // A quantifiable measure of trust or standing
    }
}

namespace webapi.Models.DND
{
    public class FeatureAndTrait
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty; // Class, Race, etc.
        public int LevelRequired { get; set; }
    }
}

namespace webapi.Models.DND
{
    public class FeatureAndTrait
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; } // Class, Race, etc.
        public int LevelRequired { get; set; }
    }
}

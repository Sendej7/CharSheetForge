namespace webapi.Models.DND
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Weapon, Armor, Potion, etc.
        public string Description { get; set; }
        public int Weight { get; set; }
        public int? Damage { get; set; } // Only for weapons
        public int? ArmorClass { get; set; } // Only for armor
        public string SpecialProperties { get; set; } // Any special abilities or conditions
    }
}

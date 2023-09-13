namespace webapi.Models.DND
{
    public class AttackAndSpellcasting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Attack or Spell
        public int Range { get; set; }
        public int Damage { get; set; }
        public string DamageType { get; set; } // Fire, Cold, etc.
        public string Components { get; set; } // V, S, M for spells
        public string Duration { get; set; } // Only for spells
    }
}

using webapi.Models.Generics;

namespace webapi.Models.DND
{
    public class AttackAndSpellcasting : ICharacterAssociable<AttackAndSpellcasting>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;// Attack or Spell
        public int Range { get; set; }
        public int Damage { get; set; }
        public string DamageType { get; set; } = string.Empty;// Fire, Cold, etc.
        public string Components { get; set; } = string.Empty;// V, S, M for spells
        public string Duration { get; set; } = string.Empty;// Only for spells
        public List<CharacterAssociation<AttackAndSpellcasting>> Associations { get; set; }
    }
}

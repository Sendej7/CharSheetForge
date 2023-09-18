namespace webapi.Models
{
    public class BaseCharacter : ICharacter
    {
        public int ID { get; set; }
        public int UserToken { get; set; }
        public int CardToken { get; set; }
        public string CharacterName { get; set; } = string.Empty;
        public int Level { get; set; }
        public User? User { get; set; }
        public SystemType SystemType { get; set; }
        public string? Discriminator { get; private set; }
    }
}

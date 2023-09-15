namespace webapi.DTO
{
    public class CreateUserRequest
    {
        public UserDto User { get; set; } = new UserDto();
        public DndCharacterDto? DndCharacter { get; set; }
    }
}

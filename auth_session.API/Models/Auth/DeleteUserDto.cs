namespace auth_session.API.Models.Auth
{
    public class DeleteUserDto(int id)
    {
        public required int Id { get; set; } = id;
    }
}
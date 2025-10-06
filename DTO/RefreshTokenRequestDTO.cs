namespace Fundsy_backend.DTO
{
    public class RefreshTokenRequestDTO
    {
        public Guid Id { get; set; }
        public required string RefreshToken { get; set; }
    }
}

namespace Fundsy_backend.DTO
{
    public class RefreshTokenRequestDTO
    {
        public string? Email { get; set; }
        public required string RefreshToken { get; set; }
    }
}

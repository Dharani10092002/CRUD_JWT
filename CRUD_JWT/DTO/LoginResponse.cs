namespace CRUD_JWT.DTO
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

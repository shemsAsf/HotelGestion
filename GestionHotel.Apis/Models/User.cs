namespace Models
{
    public class User
    {
        public required int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Roles Role { get; set; }
    }
}

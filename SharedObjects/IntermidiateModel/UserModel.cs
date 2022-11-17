


namespace SharedObjects.IntermidiateModel
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTimeOffset? JoinDate { get; set; }
        public string? Organization { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? DisplayName { get; set; }
    }
}
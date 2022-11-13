namespace IdentityShared
{
    public class UsersManager
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<Users> Users { get; set; }
    }

    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

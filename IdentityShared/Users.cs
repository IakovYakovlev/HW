namespace IdentityShared
{
    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }

    public class AllUsers
    {
        public IEnumerable<Users> Users { get; set; }
    }
}

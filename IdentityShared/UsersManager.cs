using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

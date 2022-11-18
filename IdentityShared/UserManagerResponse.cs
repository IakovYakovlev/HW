using System.Collections.Generic;

namespace IdentityShared
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}

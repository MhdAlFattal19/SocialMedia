using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Domain.Requests
{
    public class RegisterUserRequest
    {
        // to do add some validateion
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
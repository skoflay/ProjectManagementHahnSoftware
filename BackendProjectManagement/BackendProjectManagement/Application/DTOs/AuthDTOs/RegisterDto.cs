using System.ComponentModel.DataAnnotations;

namespace BackendProjectManagement.Application.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(1)]
        public string Password { get; set; }
    }
}
